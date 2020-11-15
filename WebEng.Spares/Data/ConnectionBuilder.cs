using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEng.Spares.Data
{
    public static class ConnectionBuilder
    {
        public static string GetConnectionString()
        {
            var host = "70fcc97b-e545-4085-b935-6b79a41d17d1.bkvfu0nd0m8k95k94ujg.databases.appdomain.cloud";
            var db = "ibmclouddb";
            var user = "ibm_cloud_a2c560b0_4e53_4581_b413_f8a89e8ec729";
            var passwd = "b2adee2302917d367b9d5c5846e52add71e93c7da08b1f7c13b41f50c3a67893";
            var port = 32048;
            var sslmode = "Require";
            var cert = "./cert.cert";
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4};SSL Mode={5};Client Certificate={6};Trust Server Certificate=true",
                host, db, user, passwd, port, sslmode, cert);
            return connStr;
        }
    }
}
