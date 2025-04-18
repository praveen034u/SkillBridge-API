﻿namespace SB.Domain
{
    public class AzureCognitiveSearch
    {
        public string ServiceName { get; set; }
        public string IndexName { get; set; }
        public string IndexNameJob { get; set; }
        public string ApiKey { get; set; }
        public string UserIndexName { get; set; }
        public string Endpoint => $"https://{ServiceName}.search.windows.net";

    }
}
