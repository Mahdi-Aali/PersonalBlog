namespace PersonalBlog.CategoryService.Api.Helpers;

public class ApiResponse<TPayload>
{
    public ApiResponseHeader Headers { get; set; } = null!;
    public ApiResponsePayload<TPayload> Payload { get; set; } = null!;
}

public record ApiResponseHeader(int statusCode, string[] messages);

public record ApiResponsePayload<TResult>(TResult Body);


public class ApiResponseBuilder<TPayload>
{
    private ApiResponse<TPayload> response = new();

    public ApiResponseBuilder<TPayload> SetHeaders(ApiResponseHeader headers)
    {
        response.Headers = headers;
        return this;
    }

    public ApiResponseBuilder<TPayload> SetPayload(ApiResponsePayload<TPayload> payload)
    {
        response.Payload = payload;
        return this;
    }


    public ApiResponse<TPayload> Build()
    {
        return response;
    }
}