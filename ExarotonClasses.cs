using System.Text.Json;

public enum ServerStatusCodes
{
    OFFLINE,
    ONLINE,
    STARTING,
    STOPPING,
    RESTARTING,
    SAVING,
    LOADING,
    CRASHED,
    PENDING,
    TRANSFERRING,
    PREPARING,

}

interface IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
}

public class ExarotonException : Exception
{
    public ExarotonException(string message) : base(message) { }
}

public class Players
{
    public int Max { get; set; }
    public int Count { get; set; }
    public List<string> List { get; set; }
}

public class Software
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
}

public class AccountResponse : IExarotonResponse
{
    public class Account
    {
        public string Name { get; set;  }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public int Credits { get; set; }
    }
    
    public bool Success { get; set; }
    public string Error { get; set; }
    public Account Data { get; set; }
}

public class Server
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Motd { get; set; }
    public ServerStatusCodes Status { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public Players Players { get; set; }
    public Software Software { get; set; }
    public bool Shared { get; set; }
}

public class RAM
{
    public int Ram { get; set; }
}

public class MOTD
{
    public string Motd { get; set; }
}

public class CreditPool
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float Credits { get; set; }
    public int Servers { get; set; }
    public string Owner { get; set; }
    public bool IsOwner { get; set; }
    public int Members { get; set; }
    public float OwnShare { get; set; }
    public float OwnCredits { get; set; }
}

public class ServersResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<Server> Data { get; set; }
}

public class GenericResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public string Data { get; set; }
}

public class ListResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<string> Data { get; set; }
}

public class ServerResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public Server Data { get; set; }
}

public class LogResponse : IExarotonResponse
{
    public class Log
    {
        public string Content { get; set;  }
    }
    public bool Success { get; set; }
    public string Error { get; set; }
    public Log Data { get; set; }
}

public class UploadedLogResponse : IExarotonResponse
{
    public class UploadedLog
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Raw { get; set; }
    }
    public bool Success { get; set; }
    public string Error { get; set; }
    public UploadedLog Data { get; set; }
}

public class RamResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public RAM Data { get; set; }
}

public class ChangeRamResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public RAM Data { get; set; }
}

public class MotdResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public MOTD Data { get; set;  }
}

public class ChangeMotdResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public MOTD Data { get; set; }
}

public class FileInformationResponse : IExarotonResponse
{
    public class File
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsTextFile { get; set; }
        public bool IsConfigFile { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsLog { get; set; }
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public int Size { get; set;  }
        public List<File> Children { get; set; }
    }
    public bool Success { get; set; }
    public string Error { get; set; }
    public File Data { get; set; }
}

public class ConfigOptionsResponse : IExarotonResponse
{
    public class ConfigOptions
    {
        public string Key { get; set; }
        public JsonElement Value { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public List<string> Options { get; set; }
    }
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<ConfigOptions> Data { get; set; }
}

public class CreditPoolsResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public new List<CreditPool> Data { get; set; }
}

public class CreditPoolResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public CreditPool Data { get; set; }
}

public class CreditPoolMembersResponse : IExarotonResponse
{
    public class CreditPoolMemberData
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public float Share { get; set; }
        public float Credits { get; set; }
        public bool IsOwner { get; set; }
    }
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<CreditPoolMemberData> Data { get; set; }
}

public class CreditPoolServersResponse : IExarotonResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<Server> Data { get; set; }
}