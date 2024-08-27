using System.Net;
using AutoFixture;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.Sheba;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
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

    [Fact]
    public async Task GetShebaInquiry_ReturnResponse_WhenResponseIsCorrect()
    {
        //Arrange
        var accountNumber = "123456789";
        var apiKey = "testApiKey";
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{\"HasError\":false, \"Result\": {\"result\": \"{\\\"RsCode\\\":0}\"}}")
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri == new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponseMessage).Verifiable();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")
        };
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        //Action
        var _sut = new InquiryBankHttp(_bankSettingMock.Object, _httpClientFactoryMock.Object);
        var result = await _sut.GetSebaInquiry(accountNumber, apiKey);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri == new Uri("https://api.pod.ir/srv/sc/nzh/doServiceCall")),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}
