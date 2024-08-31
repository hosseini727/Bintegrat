using System.Net;
using System.Reflection;
using AutoFixture;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.Sheba;
using BankIntegration.Service.MiddleWare.Exception;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace BankIntegration.UnitTest.InfraTest.HttpTest;

public class InquiryHttpTest
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IOptions<BankSettingModel>> _bankSettingMock;
    private readonly BankSettingModel _bankSettingModel;

    public InquiryHttpTest()
    {
        var fixture = new Fixture();

        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _bankSettingModel = fixture.Create<BankSettingModel>();
        _bankSettingMock = new Mock<IOptions<BankSettingModel>>();
        _bankSettingMock.Setup(x => x.Value).Returns(_bankSettingModel);
    }

    // mock IHttpFactory
    // [Fact]
    //  public async Task GetShebaInquiry_ReturnResponse_WhenResponseIsCorrect()
    //  {
    //      //Arrange
    //      var accountNumber = "123456789";
    //      var apiKey = "testApiKey";
    //      var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
    //      {
    //          Content = new StringContent("{\"HasError\":false, \"Result\": {\"result\": \"{\\\"RsCode\\\":0}\"}}")
    //      };
    //
    //      _httpMessageHandlerMock.Protected()
    //          .Setup<Task<HttpResponseMessage>>(
    //              "SendAsync",
    //              ItExpr.Is<HttpRequestMessage>(req =>
    //                  req.Method == HttpMethod.Post &&
    //                  req.RequestUri == new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")),
    //              ItExpr.IsAny<CancellationToken>()
    //          )
    //          .ReturnsAsync(httpResponseMessage).Verifiable();
    //
    //      var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
    //      {
    //          BaseAddress = new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")
    //      };
    //      _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
    //
    //      //Action
    //      var _sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
    //      var result = await _sut.GetSebaInquiry(accountNumber, apiKey);
    //
    //      //Assert
    //      Assert.NotNull(result);
    //      Assert.True(result.IsSuccess);
    //
    //      _httpMessageHandlerMock.Protected().Verify(
    //          "SendAsync",
    //          Times.Once(),
    //          ItExpr.Is<HttpRequestMessage>(req =>
    //              req.Method == HttpMethod.Post &&
    //              req.RequestUri == new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")),
    //          ItExpr.IsAny<CancellationToken>()
    //      );
    //  }

    [Fact]
    public async Task ParseShebaInquiryResponse_ShouldReturnSuccess_WhenResponseIsSuccessful()
    {
        // Arrange
        var responseContent = "{\"HasError\":false, \"Result\": {\"result\": \"{\\\"RsCode\\\":0, \\\"isSuccess\\\":true}\"}}";
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var methodInfo = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result =
            (Task<ApiResponseModel<FinalResponseInquery>>)methodInfo.Invoke(sut, new object[] { httpResponseMessage });
        var apiResponse = await result;

        // Assert
        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.IsSuccess);
        Assert.NotNull(apiResponse.Data);
    }

    [Fact]
    public async Task ParseShebaInquiry_ShouldReturnFailure_WhenFirstLayerResponseHasError()
    {
        //Arrange
        var responseContent = "{\"HasError\":true, \"Result\": {\"result\": \"{\\\"RsCode\\\":0}\"}}";
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };
        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);

        var mockMethodBehaviour = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);
        //Act
        var result =
            await (Task<ApiResponseModel<FinalResponseInquery>>)mockMethodBehaviour.Invoke(sut,
                new object[] { httpResponseMessage });
        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
       
    }

    [Fact]
    public async Task ParseShebaInquiry_ShouldReturnFailure_WhenApiResponseIsBadRequest()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Bad Request")
        };

        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var method = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result =
            await (Task<ApiResponseModel<FinalResponseInquery>>)method.Invoke(sut,
                new object[] { httpResponseMessage });

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.HttpStatus);
    }

    [Fact]
    public async Task ParseShebaInquiry_ShouldReturnFailure_WhenApiResponseHasNullContent()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            RequestMessage = new HttpRequestMessage { Content = null }
        };

        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var method = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result =
            await (Task<ApiResponseModel<FinalResponseInquery>>)method.Invoke(sut,
                new object[] { httpResponseMessage });

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Message);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.HttpStatus);
    }

    [Fact]
    public async Task ParseShebaInquiry_ShouldDeserializeNestedJsonCorrectly_WhenApiResponseContainsNestedJson()
    {
        // Arrange
        var firstResponseLayer = new ShebaInquiryFirstModel
        {
            HasError = false,
            Result = new Result()
            {
                result = "{\"isSuccess\":\"true\", \"rsCode\":\"0\"}" 
            }
        };
        var responseContent = JsonConvert.SerializeObject(firstResponseLayer);

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };
        
        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var method = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = await (Task<ApiResponseModel<FinalResponseInquery>>)method.Invoke(sut, new object[] { httpResponseMessage });

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }
    
    [Fact]
    public async Task ParseShebaInquiry_ShouldReturnFalse_WhenApiResponseContainsFail()
    {
        // Arrange
        var firstResponseLayer = new ShebaInquiryFirstModel
        {
            HasError = false,
            Result = new Result()
            {
                result = "{\"isSuccess\":\"false\", \"rsCode\":\"12\", \"message\":\"خطایی رخ داده است\"}" 
            }
        };
        var responseContent = JsonConvert.SerializeObject(firstResponseLayer);

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };
        
        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var method = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = await (Task<ApiResponseModel<FinalResponseInquery>>)method.Invoke(sut, new object[] { httpResponseMessage });

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Message);
    }
    
    [Fact]
    public async Task ParseShebaInquiry_ShouldReturnFailure_WhenResultIsNull()
    {
        // Arrange
        var firstResponseLayer = new ShebaInquiryFirstModel
        {
            HasError = false,
            Result = null 
        };
        var responseContent = JsonConvert.SerializeObject(firstResponseLayer);

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        var sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var mockMethodBehavoir = typeof(InquiryBankHttp).GetMethod("ParseShebaInquiryResponse",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = await (Task<ApiResponseModel<FinalResponseInquery>>)mockMethodBehavoir.Invoke(sut, new object[] { httpResponseMessage });

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
    }
}