Test Azure Function
===========================
### There three azure functions in the WebPlanAzureFunctionApp
1. GenerateWebPlan
- Trigger by Http Request
- Output Http Response
- Output Queue for trigger other function

2. GenerateWebPlanHtml
- Trigger by Http Request
- Output Http Content
- Output Queue for trigger other function

3. ParsePlan
- Trigger by Queue
- Store data into Blob

### References:
1. [Using .NET class libraries with Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library)
2. [HTTP Method Definitions](https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html)
3. [Durable Functions](https://azure.github.io/azure-functions-durable-extension/articles/overview.html)
