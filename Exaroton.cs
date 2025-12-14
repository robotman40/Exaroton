using System.Text.Json;


/// <summary>
///  The Exaroton class provides methods to interact with the Exaroton API.
/// </summary>
public class Exaroton
{
    private HttpClient sharedClient; // Shared HttpClient instance for making API requests
    private const string baseUri = "https://api.exaroton.com/v1"; // Base URI for the Exaroton API

    /// <summary>
    /// JSON serializer options to handle camelCase property naming.
    /// </summary>
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    /// <summary>
    /// Constructor for the Exaroton class.
    /// Initializes the HttpClient with the provided API key.
    /// </summary>
    /// <param name="apiKey"></param>
    public Exaroton(string apiKey)
    {
        sharedClient = new HttpClient(); // Initialize the shared HttpClient
        sharedClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); // Add the API key to the request headers
    }

    /// <summary>
    /// Helper function that makes a GET request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    private async Task<string> GetRequest(string path)
    {
        var response = await sharedClient.GetAsync($"{baseUri}/{path}"); // Make the GET request
        string responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string

        return responseString;
    }
    
    /// <summary>
    /// Helper function that makes a POST request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PostRequest(string path, HttpContent content)
    {
        var response = await sharedClient.PostAsync($"{baseUri}/{path}", content); // Make the POST request
        string responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string

        return responseString;
    }

    /// <summary>
    /// Helper function that makes a PUT request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PutRequest(string path, HttpContent content)
    {
        var response = await sharedClient.PutAsync($"{baseUri}/{path}", content); // Make the PUT request
        string responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string

        return responseString;
    }
    
    /// <summary>
    /// Helper function that makes a DELETE request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> DeleteRequest(string path, HttpContent content)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, $"{baseUri}/{path}") { Content = content }; // Create the DELETE request along with content
        var response = await sharedClient.SendAsync(request); // Send the DELETE request
        string responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string

        return responseString;
    }

    /// <summary>
    /// Internal logic for fetching Exaroton account information.
    /// </summary>
    private async Task<AccountResponse> InternalGetAccount()
    {
        string responseString = await GetRequest("account/"); // Make the GET request to the account endpoint
        var account = JsonSerializer.Deserialize<AccountResponse>(responseString, jsonOptions); // Deserialize the response into an AccountResponse object
        return account!;
    }

    /// <summary>
    /// Fetches Exaroton account information
    /// </summary>
    /// <returns>AccountResponse</returns>
    public AccountResponse GetAccount()
    {
        return InternalGetAccount().Result;
    }

    /// <summary>
    /// Internal logic for fetching the list of servers associated with the account.
    /// </summary>
    public async Task<ServersResponse> InternalGetServers()
    {
        string responseString = await GetRequest("servers/"); // Make the GET request to the servers endpoint
        var servers = JsonSerializer.Deserialize<ServersResponse>(responseString, jsonOptions); // Deserialize the response into a ServersResponse object
        return servers!;
    }
    
    /// <summary>
    /// Fetches the list of servers associated with the account.
    /// </summary>
    /// <returns>ServersResponse</returns>
    public ServersResponse GetServers()
    {
        return InternalGetServers().Result;
    }

    /// <summary>
    /// Internal logic for fetching detailed information about a specific server.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<ServerResponse> InternalGetServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/"); // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }
    
    /// <summary>
    /// Fetches detailed information about a specific server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ServerResponse</returns>
    public ServerResponse GetServer(string serverId)
    {
        return InternalGetServer(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching detailed information about a specific server.
    /// </summary>
    /// <param name="server"></param>
    private async Task<ServerResponse> InternalGetServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/"); // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }
    
    /// <summary>
    /// Fetches detailed information about a specific server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ServerResponse</returns>
    public ServerResponse GetServer(Server server)
    {
        return InternalGetServer(server).Result;
    }

    /// <summary>
    /// Internal logic for fetching the server log.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<LogResponse> InternalGetServerLog(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/logs/"); // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }

    /// <summary>
    /// Fetches the server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>LogResponse</returns>
    public LogResponse GetServerLog(string serverId)
    {
        return InternalGetServerLog(serverId).Result;
    }

    /// <summary>
    /// Internal logic for fetching the server log.
    /// </summary>
    /// <param name="server"></param>
    private async Task<LogResponse> InternalGetServerLog(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/logs/"); // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }
    
    /// <summary>
    /// Fetches the server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>LogResponse</returns>
    public LogResponse GetServerLog(Server server)
    {
        return InternalGetServerLog(server).Result;
    }

    /// <summary>
    /// Internal logic for uploading the server log.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<UploadedLogResponse> InternalUploadServerLog(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/logs/share/"); // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }
    
    /// <summary>
    /// Fetches the uploaded server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>UploadedLogResponse</returns>
    public UploadedLogResponse UploadServerLog(string serverId)
    {
        return InternalUploadServerLog(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for uploading the server log.
    /// </summary>
    /// <param name="server"></param>
    private async Task<UploadedLogResponse> InternalUploadServerLog(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/logs/share/"); // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }
    
    /// <summary>
    /// Fetches the uploaded server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>UploadedLogResponse</returns>
    public UploadedLogResponse UploadServerLog(Server server)
    {
        return InternalUploadServerLog(server).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching the server RAM option.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<RamResponse> InternalGetServerRam(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/options/ram/"); // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }
    
    /// <summary>
    /// Fetches the server RAM option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>RamResponse</returns>
    public RamResponse GetServerRam(string serverId)
    {
        return InternalGetServerRam(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching the server RAM option.
    /// </summary>
    /// <param name="server"></param>
    private async Task<RamResponse> InternalGetServerRam(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/options/ram/"); // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }
    
    /// <summary>
    /// Fetches the server RAM option.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>RamResponse</returns>
    public RamResponse GetServerRam(Server server)
    {
        return InternalGetServerRam(server).Result;
    }
    
    /// <summary>
    /// Internal logic for changing the server RAM option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverRam"></param>
    private async Task<ChangeRamResponse> InternalChangeServerRam(string serverId, int serverRam)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = serverRam }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/options/ram/", content); // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }
    
    /// <summary>
    /// Changes the server RAM option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="ram"></param>
    /// <returns>ChangeRamResponse</returns>
    public ChangeRamResponse ChangeServerRam(string serverId, int ram)
    {
        return InternalChangeServerRam(serverId, ram).Result;
    }
    
    /// <summary>
    /// Internal logic for changing the server RAM option.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="ram"></param>
    private async Task<ChangeRamResponse> InternalChangeServerRam(Server server, int ram)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = ram }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/options/ram/", content); // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }
    
    /// <summary>
    /// Changes the server RAM option.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="ram"></param>
    /// <returns>ChangeRamResponse</returns>
    public ChangeRamResponse ChangeServerRam(Server server, int ram)
    {
        return InternalChangeServerRam(server, ram).Result;
    }

    /// <summary>
    ///  Internal logic for fetching the server MOTD option.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<MotdResponse> InternalGetServerMotd(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/options/motd/"); // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }
    
    /// <summary>
    /// Fetches the server MOTD option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>MotdResponse</returns>
    public MotdResponse GetServerMotd(string serverId)
    {
        return InternalGetServerMotd(serverId).Result;
    }

    /// <summary>
    /// Internal logic for fetching the server MOTD option.
    /// </summary>
    /// <param name="server"></param>
    private async Task<MotdResponse> InternalGetServerMotd(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/options/motd/"); // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }

    /// <summary>
    /// Fetches the server MOTD option.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>MotdResponse</returns>
    public MotdResponse GetServerMotd(Server server)
    {
        return InternalGetServerMotd(server).Result;
    }
    
    /// <summary>
    /// Internal logic for changing the server MOTD option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverMotd"></param>
    private async Task<ChangeMotdResponse> InternalChangeServerMotd(string serverId, string serverMotd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = serverMotd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/options/motd/", content); // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }
    
    /// <summary>
    /// Changes the server MOTD option.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="motd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public ChangeMotdResponse ChangeServerMotd(string serverId, string motd)
    {
        return InternalChangeServerMotd(serverId, motd).Result;
    }
    
    /// <summary>
    /// Internal logic for changing the server MOTD option.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="motd"></param>
    private async Task<ChangeMotdResponse> InternalChangeServerMotd(Server server, string motd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = motd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/options/motd/", content); // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }
    
    /// <summary>
    /// Changes the server MOTD option.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="motd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public ChangeMotdResponse ChangeServerMotd(Server server, string motd)
    {
        return InternalChangeServerMotd(server, motd).Result;
    }
    
    /// <summary>
    /// Internal logic for starting a server.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<GenericResponse> InternalStartServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/start/"); // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
    
    /// <summary>
    /// Starts the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServer(string serverId)
    {
        return InternalStartServer(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for starting a server.
    /// </summary>
    /// <param name="server"></param>
    private async Task<GenericResponse> InternalStartServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/start/"); // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
    
    /// <summary>
    /// Starts the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServer(Server server)
    {
        return InternalStartServer(server).Result;
    }
    
    /// <summary>
    /// Internal logic for starting a server using own credits.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<GenericResponse> InternalStartServerWithOwnCredits(string serverId)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/start/", content); // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
    
    /// <summary>
    /// Starts the specified server using own credits.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServerWithOwnCredits(string serverId)
    {
        return InternalStartServerWithOwnCredits(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for starting a server using own credits.
    /// </summary>
    /// <param name="server"></param>
    private async Task<GenericResponse> InternalStartServerWithOwnCredits(Server server)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/start/", content); // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
    
    /// <summary>
    /// Starts the specified server using own credits.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServerWithOwnCredits(Server server)
    {
        return InternalStartServerWithOwnCredits(server).Result;
    }
    
    /// <summary>
    /// Internal logic for stopping a server.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<GenericResponse> InternalStopServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/stop/");
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions);
        return stopServer!;
    }

    /// <summary>
    /// Stops the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StopServer(string serverId)
    {
        return InternalStopServer(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for stopping a server.
    /// </summary>
    /// <param name="server"></param>
    private async Task<GenericResponse> InternalStopServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/stop/"); // Make the GET request to stop the server
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return stopServer!;
    }
    
    /// <summary>
    /// Stops the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StopServer(Server server)
    {
        return InternalStopServer(server).Result;
    }

    /// <summary>
    /// Internal logic for restarting a server.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<GenericResponse> InternalRestartServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/restart/"); // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }
    
    /// <summary>
    /// Restarts the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse RestartServer(string serverId)
    {
        return InternalRestartServer(serverId).Result;
    }

    /// <summary>
    /// Internal logic for restarting a server.
    /// </summary>
    /// <param name="server"></param>
    private async Task<GenericResponse> InternalRestartServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/restart/"); // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }
    
    /// <summary>
    /// Restarts the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse RestartServer(Server server)
    {
        return InternalRestartServer(server).Result;
    }

    /// <summary>
    /// Internal logic for executing a server command.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverCommand"></param>
    private async Task<GenericResponse> InternalExecuteServerCommand(string serverId, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/command/", content); // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }
    
    /// <summary>
    /// Executes a server command in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExecuteServerCommand(string serverId, string serverCommand)
    {
        return InternalExecuteServerCommand(serverId, serverCommand).Result;
    }

    /// <summary>
    /// Internal logic for executing a server command.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverCommand"></param>
    private async Task<GenericResponse> InternalExecuteServerCommand(Server server, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/command/", content); // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }
    
    /// <summary>
    /// Executes a server command in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExecuteServerCommand(Server server, string serverCommand)
    {
        return InternalExecuteServerCommand(server, serverCommand).Result;
    }
    
    /// <summary>
    /// Internal logic for extending the server timer.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverSeconds"></param>
    private async Task<GenericResponse> InternalExtendServerTimer(string serverId, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/extend-time/", content); // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }
    
    /// <summary>
    /// Extends the server timer in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>Generic Response</returns>
    public GenericResponse ExtendServerTimer(string serverId, int serverSeconds)
    {
        return InternalExtendServerTimer(serverId, serverSeconds).Result;
    }
    
    /// <summary>
    /// Internal logic for extending the server timer.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverSeconds"></param>
    private async Task<GenericResponse> InternalExtendServerTimer(Server server, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/extend-time/", content); // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }
    
    /// <summary>
    /// Extends the server timer in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExtendServerTimer(Server server, int serverSeconds)
    {
        return InternalExtendServerTimer(server, serverSeconds).Result;
    }
    
    // Player list methods
    
    /// <summary>
    /// Internal logic for fetching player lists.
    /// </summary>
    /// <param name="serverId"></param>
    private async Task<ListResponse> InternalGetPlayerLists(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/playerlists/"); // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }
    
    /// <summary>
    /// Fetches player lists in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerLists(string serverId)
    {
        return InternalGetPlayerLists(serverId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching player lists.
    /// </summary>
    /// <param name="server"></param>
    private async Task<ListResponse> InternalGetPlayerLists(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/playerlists/"); // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }
    
    /// <summary>
    /// Fetches player lists in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerLists(Server server)
    {
        return InternalGetPlayerLists(server).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching player list contents.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    private async Task<ListResponse> InternalGetPlayerListContents(string serverId, string list)
    {
        string responseString = await GetRequest($"servers/{serverId}/playerlists/{list}/"); // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }
    
    /// <summary>
    /// Fetches player list contents in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerListContents(string serverId, string list)
    {
        return InternalGetPlayerListContents(serverId, list).Result;
    }

    /// <summary>
    /// Internal logic for fetching player list contents.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    private async Task<ListResponse> InternalGetPlayerListContents(Server server, string list)
    {
        string responseString = await GetRequest($"servers/{server.Id}/playerlists/{list}/"); // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }
    
    /// <summary>
    /// Internal logic for fetching player list contents.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerListContents(Server server, string list)
    {
        return InternalGetPlayerListContents(server, list).Result;
    }

    /// <summary>
    /// Internal logic for adding entries to a player list.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    private async Task<ListResponse> InternalAddEntriesToPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PutRequest($"servers/{serverId}/playerlists/{list}/", content); // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }
    
    /// <summary>
    /// Adds entries to a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse AddEntriesToPlayerList(string serverId, string list, List<string> serverEntries)
    {
        return InternalAddEntriesToPlayerList(serverId, list, serverEntries).Result;
    }
    
    /// <summary>
    /// Internal logic for adding entries to a player list.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    private async Task<ListResponse> InternalAddEntriesToPlayerList(Server server, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PutRequest($"servers/{server.Id}/playerlists/{list}/", content); // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries;
    }
    
    /// <summary>
    /// Adds entries to a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse AddEntriesToPlayerList(Server server, string list, List<string> serverEntries)
    {
        return InternalAddEntriesToPlayerList(server, list, serverEntries).Result;
    }
    
    /// <summary>
    /// Internal logic for removing entries from a player list.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    private async Task<ListResponse> InternalRemoveEntriesFromPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await DeleteRequest($"servers/{serverId}/playerlists/{list}/", content); // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }
    
    /// <summary>
    /// Removes entries from a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse RemoveEntriesFromPlayerList(string serverId, string list, List<string> serverEntries)
    {
        return InternalRemoveEntriesFromPlayerList(serverId, list, serverEntries).Result;
    }

    /// <summary>
    /// Internal logic for removing entries from a player list.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    private async Task<ListResponse> InternalRemoveEntriesFromPlayerList(Server server, string list,
        List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await DeleteRequest($"servers/{server.Id}/playerlists/{list}/", content); // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }
    
    /// <summary>
    /// Removes entries from a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse RemoveEntriesFromPlayerList(Server server, string list, List<string> serverEntries)
    {
        return InternalRemoveEntriesFromPlayerList(server, list, serverEntries).Result;
    }
    
    // Get file information methods
    
    /// <summary>
    /// Internal logic for fetching file information.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    private async Task<FileInformationResponse> InternalGetFileInformation(string serverId, string filePath)
    {
        string responseString = await GetRequest($"servers/{serverId}/files/info/{filePath}/"); // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }
    
    /// <summary>
    /// Fetches file information from the specified server and file path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    /// <returns><FileInformationResponse/returns>
    public FileInformationResponse GetFileInformation(string serverId, string filePath)
    {
        return InternalGetFileInformation(serverId, filePath).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching file information.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    private async Task<FileInformationResponse> InternalGetFileInformation(Server server, string filePath)
    {
        string responseString = await GetRequest($"servers/{server.Id}/files/info/{filePath}/"); // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }
    
    /// <summary>
    /// Fetches file information from the specified server and file path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    /// <returns>FileInformationResponse</returns>
    public FileInformationResponse GetFileInformation(Server server, string filePath)
    {
        return InternalGetFileInformation(server, filePath).Result;
    }

    // Implement the file data methods later
    
    // Config file methods
    
    /// <summary>
    /// Internal logic for fetching config options.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>Task<ConfigOptionsResponse</returns>
    private async Task<ConfigOptionsResponse> InternalGetConfigOptions(string serverId, string path)
    {
        var content = await GetRequest($"servers/{serverId}/files/config/{path}/"); // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }

    /// <summary>
    /// Fetches config options from the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>GetConfigOptions</returns>
    public ConfigOptionsResponse GetConfigOptions(string serverId, string path)
    {
        return InternalGetConfigOptions(serverId, path).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching config options.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    private async Task<ConfigOptionsResponse> InternalGetConfigOptions(Server server, string path)
    {
        var content = await GetRequest($"servers/{server.Id}/files/config/{path}/"); // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }
    
    /// <summary>
    /// Fetches config options from the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse GetConfigOptions(Server server, string path)
    {
        return InternalGetConfigOptions(server, path).Result;
    }
    
    /// <summary>
    /// Internal logic for updating config options.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    private async Task<ConfigOptionsResponse> InternalUpdateConfigOptions(string serverId, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8,
            "application/json"); // Create the request content
        var response = await PostRequest($"servers/{serverId}/files/config/{path}/", content); // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    /// <summary>
    /// Updates config options for the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse UpdateConfigOptions(string serverId, string path, Dictionary<string, object> configOptions)
    {
        return InternalUpdateConfigOptions(serverId, path, configOptions).Result;
    }
    
    /// <summary>
    /// Intenral logic for updating config options.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    private async Task<ConfigOptionsResponse> InternalUpdateConfigOptions(Server server, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8, "application/json");
        Console.WriteLine(content.ReadAsStringAsync().Result); // Create the request content
        var response = await PostRequest($"servers/{server.Id}/files/config/{path}/", content); // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    /// <summary>
    /// Updates config options for the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse UpdateConfigOptions(Server server, string path, Dictionary<string, object> configOptions)
    {
        return InternalUpdateConfigOptions(server, path, configOptions).Result;
    }
    
    // Credit pool methods
    
    /// <summary>
    /// Internal logic for fetching credit pools.
    /// </summary>
    private async Task<CreditPoolsResponse> InternalGetCreditPools()
    {
        string responseString = await GetRequest("billing/pools/"); // Make the GET request to fetch credit pools
        var creditPools = JsonSerializer.Deserialize<CreditPoolsResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolsResponse object
        return creditPools!;
    }
    
    /// <summary>
    /// Fetches credit pools.
    /// </summary>
    /// <returns>CreditPoolsResponse</returns>
    public CreditPoolsResponse GetCreditPools()
    {
        return InternalGetCreditPools().Result;
    }
    
    /// <summary>
    /// Internal logic for fetching a credit pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>Task<CreditPoolResponse</returns>
    private async Task<CreditPoolResponse> InternalGetCreditPool(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/"); // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Fetches a credit pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolResponse</returns>
    public CreditPoolResponse GetCreditPool(string poolId)
    {
        return InternalGetCreditPool(poolId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching a credit pool.
    /// </summary>
    /// <param name="pool"></param>
    private async Task<CreditPoolResponse> InternalGetCreditPool(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/"); // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Fetches a credit pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolResponse</returns>
    public CreditPoolResponse GetCreditPool(CreditPool pool)
    {
        return InternalGetCreditPool(pool).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching credit pool members for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    private async Task<CreditPoolMembersResponse> InternalGetCreditPoolMembers(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/members/"); // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }

    /// <summary>
    /// Fetches credit pool members for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public CreditPoolMembersResponse GetCreditPoolMembers(string poolId)
    {
        return InternalGetCreditPoolMembers(poolId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching credit pool members for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    private async Task<CreditPoolMembersResponse> InternalGetCreditPoolMembers(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/members/"); // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }
    
    /// <summary>
    /// Fetches credit pool members for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public CreditPoolMembersResponse GetCreditPoolMembers(CreditPool pool)
    {
        return InternalGetCreditPoolMembers(pool).Result;
    }

    /// <summary>
    /// Internal logic for fetching credit pool servers for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    private async Task<CreditPoolServersResponse> InternalGetCreditPoolServers(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/servers/"); // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
    
    /// <summary>
    /// Fetches credit pool servers for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public CreditPoolServersResponse GetCreditPoolServers(string poolId)
    {
        return InternalGetCreditPoolServers(poolId).Result;
    }
    
    /// <summary>
    /// Internal logic for fetching credit pool servers for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    private async Task<CreditPoolServersResponse> InternalGetCreditPoolServers(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/servers/"); // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
    
    /// <summary>
    /// Fetches credit pool servers for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public CreditPoolServersResponse GetCreditPoolServers(CreditPool pool)
    {
        return InternalGetCreditPoolServers(pool).Result;
    }
}