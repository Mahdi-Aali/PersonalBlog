export interface ApiResponse<TResult>
{
  headers: ApiResponseHeaders;
  payload: ApiResponsePayload<TResult>
}

export interface ApiResponseHeaders
{
  statusCode: number;
  messages: string[];
}

export interface ApiResponsePayload<TResult>
{
  body: TResult;
}
