# TestAzureFunction
There three azure functions in the WebPlanAzureFunctionApp
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
