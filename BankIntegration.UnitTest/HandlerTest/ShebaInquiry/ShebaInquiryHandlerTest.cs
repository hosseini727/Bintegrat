using AutoMapper;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Model.BankInquiry;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankIntegration.UnitTest.HandlerTest.ShebaInquiry;

public class ShebaInquiryHandlerTest
{
    private readonly GetInquiryShebaHandler _sut;
    private readonly Mock<IInquiryBankHttp> _mockInquiryBankHttp;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IAPIkeyService> _mockApiKeyService;
    private string _accountNumber;
    private string _apikey;

    public ShebaInquiryHandlerTest()
    {
        _mockMapper = new Mock<IMapper>();
        _mockApiKeyService = new Mock<IAPIkeyService>();
        _mockInquiryBankHttp = new Mock<IInquiryBankHttp>();
        _sut = new GetInquiryShebaHandler(_mockInquiryBankHttp.Object, _mockMapper.Object, _mockApiKeyService.Object);
        _apikey = "api-key";
        _accountNumber = "IR123456789012345678901234";
    }


    [Fact]
    public async Task Hanlde_shouldReturnResponseModel_whenRequestIsValid()
    {
        //Arrange
        var query = new GetInquiryShebaQuery(_accountNumber);
        var inquiryResult = new ApiResponseModel<FinalResponseInquery>
        {
            IsSuccess = true,
            Data = new FinalResponseInquery
            {
                AccountStatus = "02"
            }
        };

        var mappedResponse = new ShebaInquiryResponseModel
        {
            IsSuccess = true
        };

        _mockApiKeyService.Setup(x => x.GetShebaInquiryApiKey()).ReturnsAsync(_apikey);
        _mockInquiryBankHttp.Setup(x => x.GetSebaInquiry(_accountNumber, _apikey)).ReturnsAsync(inquiryResult);
        _mockMapper.Setup(x => x.Map<FinalResponseInquery, ShebaInquiryResponseModel>(inquiryResult.Data))
            .Returns(mappedResponse);
        //Act

        var result = await _sut.Handle(query, CancellationToken.None);
        //Assert

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Hanlde_shouldThrowBadRequestException_WhenResponseIsFalse()
    {
        //Arrange
        var query = new GetInquiryShebaQuery(_accountNumber);
        _mockApiKeyService.Setup(x => x.GetShebaInquiryApiKey()).ReturnsAsync(_apikey);
        var inquiryResult = new ApiResponseModel<FinalResponseInquery> { IsSuccess = false, Message = "Error" };
        _mockInquiryBankHttp.Setup(x => x.GetSebaInquiry(_accountNumber, _apikey)).ReturnsAsync(inquiryResult);
        //Act && Assert
        //await Assert.ThrowsAsync<BadRequestException>(() => _sut.Handle(query, default));
        //Act
        Func<Task> act = () => _sut.Handle(query, default);
        //Assert -- fluentAssertion
        await act.Should().ThrowAsync<BadRequestException>().WithMessage(inquiryResult.Message);
    }

    [Fact]
    public async Task Hanlde_ShouldThrowBadRequestException_whenApikeyIsNull()
    {
        //Arrange
        _mockApiKeyService.Setup(x => x.GetShebaInquiryApiKey()).ReturnsAsync((string)null!);
        var query = new GetInquiryShebaQuery(_accountNumber);
        //Act
        Func<Task> act = () => _sut.Handle(query, default);
        //Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("apikey is Null");
    }
    
    [Fact]
    public async Task Hanlde_ShouldThrowBadRequestException_whenAccountNoIsNull()
    {
        //Arrange
        _accountNumber = string.Empty;
        var query = new GetInquiryShebaQuery(_accountNumber);
        //Act
        Func<Task> act = () => _sut.Handle(query, default);
        //Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("account number is Null");
    }
    
    [Fact]
    public async Task Hanlde_ShouldThrowBadRequestException_whenAccountNoLessThan26Char()
    {
        //Arrange
        _accountNumber = "123456789";
        var query = new GetInquiryShebaQuery(_accountNumber);
        //Act
        Func<Task> act = () => _sut.Handle(query, default);
        //Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("length of  account number is not equal 26 character");
    }
    
    
    [Fact]
    public async Task Hanlde_ShouldThrowBadRequestException_whenAccountNoMoreThan26Char()
    {
        //Arrange
        _accountNumber = "123456789123456789123456789123456789";
        var query = new GetInquiryShebaQuery(_accountNumber);
        //Act
        Func<Task> act = () => _sut.Handle(query, default);
        //Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("length of  account number is not equal 26 character");
    }
    
    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenResultDataIsNull()
    {
        // Arrange
        var inquiryResult = new ApiResponseModel<FinalResponseInquery> { IsSuccess = true, Data = null! };
        _mockApiKeyService.Setup(x => x.GetShebaInquiryApiKey()).ReturnsAsync(_apikey);
        _mockInquiryBankHttp.Setup(x => x.GetSebaInquiry(_accountNumber, _apikey)).ReturnsAsync(inquiryResult);
        var query = new GetInquiryShebaQuery(_accountNumber);

        // Act
        Func<Task> act = () => _sut.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("data is null from provider");
    }
    
}