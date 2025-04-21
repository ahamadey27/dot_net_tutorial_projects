using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.CSharp;

public class SwaggerClientGenerator
{
    public async Task GenerateClient()
    {
        var httpClient = new HttpClient();
        var swaggerJson = await httpClient.GetStringAsync("http://127.0.0.1:5500/swagger/v1/swagger.json");
        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);
        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = "BlogApiClient",
            CSharpGeneratorSettings =
            {
                Namespace = "BlogApi"
            }
        };

        var generator = new CSharpClientGenerator(document, settings);
        var code = generator.GenerateFile();
        await File.WriteAllTextAsync("BlogApiClient.cs", code);

    }
}