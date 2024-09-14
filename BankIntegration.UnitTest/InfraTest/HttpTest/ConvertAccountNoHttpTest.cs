using AutoFixture;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.ConvertAccount;
using BankIntegration.Infra.ThirdApi.Sheba;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Reflection;
using Xunit;



namespace BankIntegration.UnitTest.InfraTest.HttpTest
{
   

    public class ConvertAccountNoHttpTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly Mock<IOptions<BankSettingModel>> _bankSettingMock;
        private readonly BankSettingModel _bankSettingModel;
        public ConvertAccountNoHttpTest()
        {
            var fixture = new Fixture();

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _bankSettingModel = fixture.Create<BankSettingModel>();
            _bankSettingMock = new Mock<IOptions<BankSettingModel>>();
            _bankSettingMock.Setup(x => x.Value).Returns(_bankSettingModel);
        }


        [Fact]
        public async Task ConvertAccountNoResponse_ShouldReturnSuccess_WhenResponseIsSuccessful()
        {
            // Arrange  
            var responseContent = "{\"HasError\":false, \"Result\": {\"result\": \"{\\\"errorCode\\\":0, \\\"isSuccess\\\":true}\"}}";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };

            var sut = new ConvertAccountNoBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
            var methodInfo = typeof(ConvertAccountNoBankHttp).GetMethod("ParseDepositInquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result =
                (Task<ApiResponseModel<FinalResponseDepositInquery>>)methodInfo.Invoke(sut, new object[] { httpResponseMessage });
            var apiResponse = await result;

            // Assert
            Assert.NotNull(apiResponse);
            Assert.True(apiResponse.IsSuccess);
            Assert.NotNull(apiResponse.Data);
        }

        [Fact]
        public async Task ConvertAccountNo_ShouldReturnFailure_WhenFirstLayerResponseHasError()
        {
            //Arrange
            var responseContent = "{\"HasError\":true, \"Result\": {\"result\": \"{\\\"isSuccess\\\":false}\"}}";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };
            var sut = new ConvertAccountNoBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);

            var mockMethodBehaviour = typeof(ConvertAccountNoBankHttp).GetMethod("ParseDepositInquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);
            //Act
            var result =
                await (Task<ApiResponseModel<FinalResponseDepositInquery>>)mockMethodBehaviour.Invoke(sut,
                    new object[] { httpResponseMessage });
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task ConvertAccountNo_ShouldReturnFailure_WhenApiResponseIsBadRequest()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Bad Request")
            };

            var sut = new ConvertAccountNoBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
            var method = typeof(ConvertAccountNoBankHttp).GetMethod("ParseDepositInquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result =
               await (Task<ApiResponseModel<FinalResponseDepositInquery>>)method.Invoke(sut,
                    new object[] { httpResponseMessage });

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.HttpStatus);
        }

    }

}
