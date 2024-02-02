using AngleSharp.Dom;
using medsys.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace medsys_integration_tests;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    public AuthTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    [Fact]
    public async void Post_EndpointReturnsCreated()
    {
        var client = _factory.CreateClient();
        var json = new UserRegisterDTO
        {
            LoginEmail = "userTest",
            FullName = "string",
            IsDoctor = false,
            Password = "password",
        };

        var response = await client.PostAsJsonAsync("api/register", json);
        _output.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async void Post_EndpointReturnsSuccessLoggedIn()
    {
        var client = _factory.CreateClient();
        var json = new UserLoginDTO
        {
            LoginEmail = "string",
            Password = "string"
        };
        
        var response = await client.PostAsJsonAsync("api/login", json);
        _output.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }    
}

