
# Custom Query Parameter



Documentation for accessing and setting credentials for apiKey.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| token | `string` | - | `Token` | `Token` |
| api-key | `string` | - | `ApiKey` | `ApiKey` |



**Note:** Auth credentials can be set using `ApiKeyCredentials` in the client builder and accessed through `ApiKeyCredentials` method in the client instance.

## Usage Example

### Client Initialization

You must provide credentials in the client as shown in the following code snippet.

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .ApiKeyCredentials(
        new ApiKeyModel.Builder(
            "token",
            "api-key"
        )
        .Build())
    .Build();
```


