﻿namespace api.Models.Settings;

public class MongoSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string SessionCollectionName { get; set; } = null!;
}
