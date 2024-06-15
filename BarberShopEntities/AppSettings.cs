namespace BarberShopEntities
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }

        public ConnectionStrings  ConnectionStrings { get; set; }

        public String[] Origins { get; set; }

    }

    public class ConnectionStrings
    {
        public string BarberShop { get; set; }

    }
    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
    }

    
}

