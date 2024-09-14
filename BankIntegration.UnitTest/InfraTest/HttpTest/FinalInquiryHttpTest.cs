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
   

    public class FinalInquiryHttpTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly Mock<IOptions<BankSettingModel>> _bankSettingMock;
        private readonly BankSettingModel _bankSettingModel;
        public FinalInquiryHttpTest()
        {
            var fixture = new Fixture();

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _bankSettingModel = fixture.Create<BankSettingModel>();
            _bankSettingMock = new Mock<IOptions<BankSettingModel>>();
            _bankSettingMock.Setup(x => x.Value).Returns(_bankSettingModel);
        }


        [Fact]
        public async Task ParseFinalInquiryResponse_ShouldReturnSuccess_WhenResponseIsSuccessful()
        {
            // Arrange  
            var responseContent = "{\"HasError\":false, \"Result\": {\"result\": \"{\\\"FinalState\\\":true, \\\"isSuccess\\\":true}\"}}";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };

            var sut = new FinalInquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
            var methodInfo = typeof(FinalInquiryBankHttp).GetMethod("ParseFinalinquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result =
                (Task<ApiResponseModel<FinalResponseInquiry>>)methodInfo.Invoke(sut, new object[] { httpResponseMessage });
            var apiResponse = await result;

            // Assert
            Assert.NotNull(apiResponse);
            Assert.True(apiResponse.IsSuccess);
            Assert.NotNull(apiResponse.Data);
        }

        [Fact]
        public async Task ParseFinalInquiry_ShouldReturnFailure_WhenFirstLayerResponseHasError()
        {
            //Arrange
            var responseContent = "{\"HasError\":true, \"Result\": {\"result\": \"{\\\"isSuccess\\\":false}\"}}";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };
            var sut = new FinalInquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);

            var mockMethodBehaviour = typeof(FinalInquiryBankHttp).GetMethod("ParseFinalinquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);
            //Act
            var result =
                await (Task<ApiResponseModel<FinalResponseInquiry>>)mockMethodBehaviour.Invoke(sut,
                    new object[] { httpResponseMessage });
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task ParseFinalInquiry_ShouldReturnFailure_WhenApiResponseIsBadRequest()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Bad Request")
            };

            var sut = new FinalInquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
            var method = typeof(FinalInquiryBankHttp).GetMethod("ParseFinalinquiryResponse",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result =
               await (Task<ApiResponseModel<FinalResponseInquiry>>)method.Invoke(sut,
                    new object[] { httpResponseMessage });

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.HttpStatus);
        }

    }

}
