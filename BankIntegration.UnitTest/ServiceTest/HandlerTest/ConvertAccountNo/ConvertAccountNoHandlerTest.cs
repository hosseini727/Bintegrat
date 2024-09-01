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

namespace BankIntegration.UnitTest.ServiceTest.HandlerTest.ConvertAccountNo;

public class ConvertAccountNoHandlerTest
{
    private readonly ConvertAccountNoHandler _sut;
    private readonly Mock<IConvertAccountNoBankHttp> _mockConvertAccountNoBankHttp;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IAPIkeyService> _mockApiKeyService;
    private string _accountNumber;
    private string _apikey;

    public ConvertAccountNoHandlerTest()
    {
        _mockMapper = new Mock<IMapper>();
        _mockApiKeyService = new Mock<IAPIkeyService>();
        _mockConvertAccountNoBankHttp = new Mock<IConvertAccountNoBankHttp>();
        _sut = new ConvertAccountNoHandler(_mockConvertAccountNoBankHttp.Object, _mockMapper.Object, _mockApiKeyService.Object);
        _apikey = "api-key";
        _accountNumber = "335.8000.11315156.1";
    }


    [Fact]
    public async Task Hanlde_shouldReturnResponseModel_whenRequestIsValid()
    {
        //Arrange
        var query = new ConvertAccountNoQuery(_accountNumber);
        var convertAccountNoResult = new ApiResponseModel<FinalResponseDepositInquery>
        {
            IsSuccess = true,
            Data = new FinalResponseDepositInquery
            {
                DepositNumber = _accountNumber
            }
        };

        var mappedResponse = new ConvertAccountNoResponseModel
        {
            IsSuccess = true
        };

        _mockApiKeyService.Setup(x => x.GetDepositInquiryApiKey()).ReturnsAsync(_apikey);
        _mockConvertAccountNoBankHttp.Setup(x => x.ConvertAccountNo(_accountNumber, _apikey)).ReturnsAsync(convertAccountNoResult);
        _mockMapper.Setup(x => x.Map<FinalResponseDepositInquery, ConvertAccountNoResponseModel>(convertAccountNoResult.Data))
            .Returns(mappedResponse);

        //Act
        var result = await _sut.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Handle_shouldReturnError_whenAccountNumberIsTooShort()
    {
        // Arrange
        var shortAccountNumber = "123";
        var query = new ConvertAccountNoQuery(shortAccountNumber);

        _mockApiKeyService.Setup(x => x.GetDepositInquiryApiKey()).ReturnsAsync(_apikey);
        var convertAccountNoResult = new ApiResponseModel<FinalResponseDepositInquery> { IsSuccess = false };
        _mockConvertAccountNoBankHttp.Setup(x => x.ConvertAccountNo(shortAccountNumber, _apikey)).ReturnsAsync(convertAccountNoResult);

        // Act
        Func<Task> act = () => _sut.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_shouldReturnError_whenApiCallFails()
    {
        // Arrange
        var query = new ConvertAccountNoQuery(_accountNumber);

        _mockApiKeyService.Setup(x => x.GetDepositInquiryApiKey()).ReturnsAsync(_apikey);
        _mockConvertAccountNoBankHttp.Setup(x => x.ConvertAccountNo(_accountNumber, _apikey))
            .ThrowsAsync(new Exception("API call failed"));

        // Act
        Func<Task> act = () => _sut.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Handle_shouldReturnError_whenApiResponseIsEmpty()
    {
        // Arrange
        var query = new ConvertAccountNoQuery(_accountNumber);

        _mockApiKeyService.Setup(x => x.GetDepositInquiryApiKey()).ReturnsAsync(_apikey);
        _mockConvertAccountNoBankHttp.Setup(x => x.ConvertAccountNo(_accountNumber, _apikey))
            .ReturnsAsync(default(ApiResponseModel<FinalResponseDepositInquery>));

        // Act
        Func<Task> act = () => _sut.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<Exception>(); // Or a more specific exception type based on your implementation
    }


    [Fact]
    public async Task Handle_shouldReturnError_whenApiKeyIsInvalid()
    {
        // Arrange
        var query = new ConvertAccountNoQuery(_accountNumber);

        _mockApiKeyService.Setup(x => x.GetDepositInquiryApiKey()).ReturnsAsync(default(string));

        // Act
        Func<Task> act = () => _sut.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }


}