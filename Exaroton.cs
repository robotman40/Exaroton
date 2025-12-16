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
        string responseString; // Variable to hold the response string
        try
        {
            var response = await sharedClient.GetAsync($"{baseUri}/{path}"); // Make the GET request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (HttpRequestException e)
        {
            throw new ExarotonException($"An error occurred making a GET request: {e.Message}");
        }

        return responseString;
    }
    
    /// <summary>
    /// Helper function that makes a POST request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PostRequest(string path, HttpContent content)
    {
        string responseString; // Variable to hold the response string
        try
        {
            var response = await sharedClient.PostAsync($"{baseUri}/{path}", content); // Make the POST request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a POST request: {e.Message}");
        }

        return responseString;
    }

    /// <summary>
    /// Helper function that makes a PUT request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PutRequest(string path, HttpContent content)
    {
        string responseString; // Variable to hold the response string
        try
        {
            var response = await sharedClient.PutAsync($"{baseUri}/{path}", content); // Make the PUT request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a PUT request: {e.Message}");
        }

        return responseString;
    }

    /// <summary>
    /// Helper function that makes a DELETE request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> DeleteRequest(string path, HttpContent content)
    {
        string responseString; // Variable to hold the response string
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"{baseUri}/{path}")
                { Content = content }; // Create the DELETE request along with content
            var response = await sharedClient.SendAsync(request); // Send the DELETE request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a DELETE request: {e.Message}");
        }
        
        return responseString;
    }

    /// <summary>
    /// Get account information.
    /// </summary>
    /// <returns>AccountResponse</returns>
    public async Task<AccountResponse> GetAccount()
    {
        string responseString = await GetRequest("account/"); // Make the GET request to the account endpoint
        var account = JsonSerializer.Deserialize<AccountResponse>(responseString, jsonOptions); // Deserialize the response into an AccountResponse object
        return account!;
    }

    /// <summary>
    /// Get a list of servers.
    /// </summary>
    /// <returns>ServersResponse</returns>
    public async Task<ServersResponse> GetServers()
    {
        string responseString = await GetRequest("servers/"); // Make the GET request to the servers endpoint
        var servers = JsonSerializer.Deserialize<ServersResponse>(responseString, jsonOptions); // Deserialize the response into a ServersResponse object
        return servers!;
    }

    /// <summary>
    /// Get information about a specific server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ServerResponse</returns>
    public async Task<ServerResponse> GetServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/"); // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }

    /// <summary>
    /// Get information about a specific server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ServerResponse</returns>
    public async Task<ServerResponse> GetServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/"); // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }

    /// <summary>
    /// Get the server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>LogResponse</returns>
    public async Task<LogResponse> GetServerLog(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/logs/"); // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }

    /// <summary>
    /// Get the server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>LogResponse</returns>
    public async Task<LogResponse> GetServerLog(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/logs/"); // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }

    /// <summary>
    /// Upload the server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>UploadedLogResponse</returns>
    public async Task<UploadedLogResponse> UploadServerLog(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/logs/share/"); // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }
 
    /// <summary>
    /// Upload the server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>UploadedLogResponse</returns>
    public async Task<UploadedLogResponse> UploadServerLog(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/logs/share/"); // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }

   /// <summary>
   /// Get the RAM of the specified server.
   /// </summary>
   /// <param name="serverId"></param>
   /// <returns>RamResponse</returns>
    public async Task<RamResponse> GetServerRam(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/options/ram/"); // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }

    /// <summary>
    /// Get the RAM of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>RamResponse</returns>
    public async Task<RamResponse> GetServerRam(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/options/ram/"); // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }
  
    /// <summary>
    /// Change the RAM of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverRam"></param>
    /// <returns>ChangeRamResponse</returns>
    public async Task<ChangeRamResponse> ChangeServerRam(string serverId, int serverRam)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = serverRam }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/options/ram/", content); // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }

    /// <summary>
    /// Change the RAM of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="ram"></param>
    /// <returns>ChangeRamResponse</returns>
    public async Task<ChangeRamResponse> ChangeServerRam(Server server, int ram)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = ram }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/options/ram/", content); // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }

    /// <summary>
    /// Get the MOTD of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>MotdResponse</returns>
    public async Task<MotdResponse> GetServerMotd(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/options/motd/"); // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }

    /// <summary>
    /// Get the MOTD of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>MotdResponse</returns>
    public async Task<MotdResponse> GetServerMotd(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/options/motd/"); // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }
   
    /// <summary>
    /// Change the MOTD of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverMotd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public async Task<ChangeMotdResponse> ChangeServerMotd(string serverId, string serverMotd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = serverMotd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/options/motd/", content); // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }
  
    /// <summary>
    /// Change the MOTD of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="motd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public async Task<ChangeMotdResponse> ChangeServerMotd(Server server, string motd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = motd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/options/motd/", content); // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }

    /// <summary>
    /// Start the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StartServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/start/"); // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Start the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StartServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/start/"); // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Start the specified server using own credits.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StartServerWithOwnCredits(string serverId)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/start/", content); // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
 
    /// <summary>
    /// Start the specified server using own credits.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StartServerWithOwnCredits(Server server)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/start/", content); // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Stop the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StopServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/stop/"); // Make the GET request to stop the server
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return stopServer!;
    }

    /// <summary>
    /// Stop the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> StopServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/stop/"); // Make the GET request to stop the server
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return stopServer!;
    }
    
    /// <summary>
    /// Restart the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> RestartServer(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/restart/"); // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }

    /// <summary>
    /// Restart the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> RestartServer(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/restart/"); // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }

    /// <summary>
    /// Execute a server command in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> ExecuteServerCommand(string serverId, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/command/", content); // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }

    /// <summary>
    /// Execute a server command in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> ExecuteServerCommand(Server server, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/command/", content); // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }

    /// <summary>
    /// Extend the timer for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> ExtendServerTimer(string serverId, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{serverId}/extend-time/", content); // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }

    /// <summary>
    /// Extend the timer for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> ExtendServerTimer(Server server, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PostRequest($"servers/{server.Id}/extend-time/", content); // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }
    
    // Player list methods
    
    /// <summary>
    /// Get player lists in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> GetPlayerLists(string serverId)
    {
        string responseString = await GetRequest($"servers/{serverId}/playerlists/"); // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }
    
    /// <summary>
    /// Get player lists in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> GetPlayerLists(Server server)
    {
        string responseString = await GetRequest($"servers/{server.Id}/playerlists/"); // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }

    /// <summary>
    /// Get player list contents in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> GetPlayerListContents(string serverId, string list)
    {
        string responseString = await GetRequest($"servers/{serverId}/playerlists/{list}/"); // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }
    
    /// <summary>
    /// Get player list contents in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> GetPlayerListContents(Server server, string list)
    {
        string responseString = await GetRequest($"servers/{server.Id}/playerlists/{list}/"); // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }

    /// <summary>
    /// Add entries to a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> AddEntriesToPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PutRequest($"servers/{serverId}/playerlists/{list}/", content); // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Add entries to a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> AddEntriesToPlayerList(Server server, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await PutRequest($"servers/{server.Id}/playerlists/{list}/", content); // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Remove entries from a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> RemoveEntriesFromPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await DeleteRequest($"servers/{serverId}/playerlists/{list}/", content); // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Remove entries from a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public async Task<ListResponse> RemoveEntriesFromPlayerList(Server server, string list,
        List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = await DeleteRequest($"servers/{server.Id}/playerlists/{list}/", content); // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    // Get file information methods
    
    /// <summary>
    /// Get file information from the specified server and file path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    /// <returns>FileInformationResponse</returns>
    public async Task<FileInformationResponse> GetFileInformation(string serverId, string filePath)
    {
        string responseString = await GetRequest($"servers/{serverId}/files/info/{filePath}/"); // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }

    /// <summary>
    /// Get file information from the specified server and file path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    /// <returns>FileInformationResponse</returns>
    public async Task<FileInformationResponse> GetFileInformation(Server server, string filePath)
    {
        string responseString = await GetRequest($"servers/{server.Id}/files/info/{filePath}/"); // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }
    
    /// <summary>
    /// Get file information and save it to the specified path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    /// <param name="savePath"></param>
    public async Task GetFileData(string serverId, string filePath, string savePath)
    {
        try
        {
            var fileBytes = await sharedClient.GetByteArrayAsync($"{baseUri}/servers/{serverId}/files/data/{filePath}/"); // Make the GET request to download the file
            File.WriteAllBytes(savePath, fileBytes); // Save the downloaded file to the specified path
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred while getting file data: {e.Message}");
        }
    }
    
    /// <summary>
    /// Get file information and save it to the specified path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    /// <param name="savePath"></param>
    public async Task GetFileData(Server server, string filePath, string savePath)
    {
        try
        {
            var fileBytes = await sharedClient.GetByteArrayAsync($"{baseUri}/servers/{server.Id}/files/data/{filePath}/"); // Make the GET request to download the file
            File.WriteAllBytes(savePath, fileBytes); // Save the downloaded file to the specified path
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred while getting file data: {e.Message}");
        }
    }
    
    /// <summary>
    /// Write file data to the specified server and path. 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="fileData"></param>
    /// <param name="folder"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> WriteFileData(string serverId, string path, byte[] fileData)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{serverId}/files/data/{path}/"); // Create the PUT request to upload the file
        var content = new ByteArrayContent(fileData); // Create the request content with the file data
        request.Content = content; // Set the request content
        
        var response = await sharedClient.SendAsync(request); // Make the POST request to upload the file and get the response
        var responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        var writeFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFileResponse!;
    }
    
    /// <summary>
    /// Write file data to the specified server and path. 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="fileData"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> WriteFileData(Server server, string path, byte[] fileData)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{server.Id}/files/data/{path}/"); // Create the PUT request to upload the file
        var content = new ByteArrayContent(fileData); // Create the request content with the file data
        request.Content = content; // Set the request content
        
        var response = await sharedClient.SendAsync(request); // Make the POST request to upload the file and get the response
        var responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        var writeFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFileResponse!;
    }
    
    /// <summary>
    /// Write a folder to the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> WriteFolder(string serverId, string path)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{serverId}/files/data/{path}/"); // Create the PUT request to upload the folder
        var content = new ByteArrayContent(Array.Empty<byte>()); // Create empty content for folder upload
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("inode/directory"); // Set content type to application/zip
        request.Content = content; // Set the request content
        
        var response = sharedClient.SendAsync(request).Result; // Make the POST request to upload the folder and get the response
        var responseString = response.Content.ReadAsStringAsync().Result; // Read the response content as a string
        var writeFolderResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFolderResponse!;
    }
    
    /// <summary>
    /// Write a folder to the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> WriteFolder(Server server, string path)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{server.Id}/files/data/{path}/"); // Create the PUT request to upload the folder
        var content = new ByteArrayContent(Array.Empty<byte>()); // Create empty content for folder upload
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("inode/directory"); // Set content type to application/zip
        request.Content = content; // Set the request content
        
        var response = await sharedClient.SendAsync(request); // Make the POST request to upload the folder and get the response
        var responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        var writeFolderResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFolderResponse!;
    }
    
    /// <summary>
    /// Delete a file from the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> DeleteFile(string serverId, string path)
    {
        var content = new StringContent(""); // Create empty content for DELETE request
        string responseString = await DeleteRequest($"servers/{serverId}/files/data/{path}/", content); // Make the DELETE request to delete the file
        var deleteFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return deleteFileResponse!;
    }
    
    /// <summary>
    /// Delete a file from the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public async Task<GenericResponse> DeleteFile(Server server, string path)
    {
        var content = new StringContent(""); // Create empty content for DELETE request
        string responseString = await DeleteRequest($"servers/{server.Id}/files/data/{path}/", content); // Make the DELETE request to delete the file
        var deleteFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return deleteFileResponse!;
    }
    
    // Config file methods
    
    /// <summary>
    /// Get configuration options for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public async Task<ConfigOptionsResponse> GetConfigOptions(string serverId, string path)
    {
        var content = await GetRequest($"servers/{serverId}/files/config/{path}/"); // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }
    
    /// <summary>
    /// Get configuration options for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public async Task<ConfigOptionsResponse> GetConfigOptions(Server server, string path)
    {
        var content = await GetRequest($"servers/{server.Id}/files/config/{path}/"); // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }
    
    /// <summary>
    /// Update configuration options for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public async Task<ConfigOptionsResponse> UpdateConfigOptions(string serverId, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8,
            "application/json"); // Create the request content
        var response = await PostRequest($"servers/{serverId}/files/config/{path}/", content); // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    /// <summary>
    /// Update configuration options for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public async Task<ConfigOptionsResponse> UpdateConfigOptions(Server server, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8, "application/json");
        Console.WriteLine(content.ReadAsStringAsync().Result); // Create the request content
        var response = await PostRequest($"servers/{server.Id}/files/config/{path}/", content); // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    // Credit pool methods
    
    /// <summary>
    /// Get a list of credit pools.
    /// </summary>
    /// <returns>CreditPoolsResponse</returns>
    public async Task<CreditPoolsResponse> GetCreditPools()
    {
        string responseString = await GetRequest("billing/pools/"); // Make the GET request to fetch credit pools
        var creditPools = JsonSerializer.Deserialize<CreditPoolsResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolsResponse object
        return creditPools!;
    }
    
    /// <summary>
    /// Get information about a specified credit pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolResponse</returns>
    public async Task<CreditPoolResponse> GetCreditPool(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/"); // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Get information about a specified credit pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolResponse</returns>
    public async Task<CreditPoolResponse> GetCreditPool(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/"); // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Get credit pool members for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public async Task<CreditPoolMembersResponse> GetCreditPoolMembers(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/members/"); // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }
    
    /// <summary>
    /// Get credit pool members for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public async Task<CreditPoolMembersResponse> GetCreditPoolMembers(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/members/"); // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }

    /// <summary>
    /// Get credit pool servers for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public async Task<CreditPoolServersResponse> GetCreditPoolServers(string poolId)
    {
        string responseString = await GetRequest($"billing/pools/{poolId}/servers/"); // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
    
    /// <summary>
    /// Get credit pool servers for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public async Task<CreditPoolServersResponse> GetCreditPoolServers(CreditPool pool)
    {
        string responseString = await GetRequest($"billing/pools/{pool.Id}/servers/"); // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
}