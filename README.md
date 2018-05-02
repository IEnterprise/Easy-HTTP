## Why?

Easy-HTTP will make your life easier when calling Rest APIs. The library supports full or partial class parsing, Json and Form-Data support as well as full customization if needed.

## Platform

Full support for **.Net Standard 2.0**
- Windows
- Mac OS
- Linux

## Examples

### Get request
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org/get")
.SetContentType(ContentType.Application_Json)
.SetType(RequestType.Get)
.Build()
.Execute();
```
### Query request
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org")
.SetContentType(ContentType.Application_Json)
.SetType(RequestType.Get)
.ContinueToQuery()
	.SetQuery("/get")
	.ParseModelToQuery(dto)
	.Build()
.Build()
.Execute();
```
### Post request
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org")
.SetContentType(ContentType.Application_Json)
.SetType(RequestType.Post)
.SetModelToSerialize(dto)
.Build()
.Execute();
```
### Partial Parsing
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org")
.SetContentType(ContentType.Application_Json)
.SetType(RequestType.Post)
.SetModelToSerialize(dto, true)
.Build()
.Execute();
```
> In order to use "Partial Parsing" the model has to have the \[EndpotinContract(PutInRequest=true)] attribute on the required properties 

### Adding Headers
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org")
.AddHeader(x => x.Accept_Encoding, "UTF-8")
.AddHeader(x => x.Authorization, "Bearer XXXXXXXXXXXXXXXXX")
.Build()
.Execute();
```

### Custom Headers
```
await new RequestBuilder<ExampleObject>()
.SetHost("https://httpbin.org")
.AddHeader("Custom-Header", "Header Value")
.Build()
.Execute();
```


### Default parameters
- "checkForAttribute" is set to "false"
- "ContentType" is set to "Application_Json"
- "RequestType" is set to "Get"
