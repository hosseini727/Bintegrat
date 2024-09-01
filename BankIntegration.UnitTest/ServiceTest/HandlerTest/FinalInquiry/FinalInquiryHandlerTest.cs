using AutoMapper;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.ConvertAccount;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Model.BankInquiry;
using FluentAssertions;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankIntegration.UnitTest.ServiceTest.HandlerTest.FinalInquiry;
public class FinalInquiryHandlerTest
{
    private readonly FinalInquiryQueryHandler _sut;
    private readonly Mock<IFinalInquiryBankHttp> _mockFinalBankHttp;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IAPIkeyService> _mockApiKeyService;
    private string _transactionId;
    private string _apikey;

    public FinalInquiryHandlerTest()
    {
        _mockMapper = new Mock<IMapper>();
        _mockApiKeyService = new Mock<IAPIkeyService>();
        _mockFinalBankHttp = new Mock<IFinalInquiryBankHttp>();
        _sut = new FinalInquiryQueryHandler(_mockFinalBankHttp.Object, _mockMapper.Object, _mockApiKeyService.Object);
        _apikey = "api-key";
        _transactionId = "4321-0002178576-20240528113427249-1590";
    }


    [Fact]
    public async Task Hanlde_shouldReturnResponseModel_whenRequestIsValid()
    {
        //Arrange
        var query = new FinalInquiryQuery(_transactionId);
        var convertAccountNoResult = new ApiResponseModel<FinalResponseInquiry>
        {
            IsSuccess = true,
            Data = new FinalResponseInquiry
            {
                TransactionMessage = _transactionId
            }
        };

        var mappedResponse = new FinalInquiryResponseModel
        {
            IsSuccess = true
        };

        _mockApiKeyService.Setup(x => x.GetFinalInquiryApiKey()).ReturnsAsync(_apikey);
        _mockFinalBankHttp.Setup(x => x.FinalInquiry(_transactionId, _apikey)).ReturnsAsync(convertAccountNoResult);
        _mockMapper.Setup(x => x.Map<FinalResponseInquiry, FinalInquiryResponseModel>(convertAccountNoResult.Data))
            .Returns(mappedResponse);

        //Act
        var result = await _sut.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
    }


}