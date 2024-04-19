using RefitClientGeneratedModels.Clients;

namespace RefitClientGeneratedModels;

public class QuestionApiTests
{
    private readonly IQuestionApi _questionApi;

#pragma warning disable xUnit1041
    public QuestionApiTests(IQuestionApi questionApi)
#pragma warning restore xUnit1041
    {
        _questionApi = questionApi;
    }

    [Fact]
    public async Task Test1()
    {
        var questions = await _questionApi.GetQuestionsAsync();
    }
}
