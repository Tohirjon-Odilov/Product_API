using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        public string ConnectionString = "Host=localhost; Port = 5432; Database = Market; User Id = postgres; Password = pinkod;";
        public NpgsqlConnection connection;
        public ProductPostgressRepository()
        {
            connection = new NpgsqlConnection(ConnectionString);
        }

        public Product Add(Product product)
        {
            connection.Open();
            try
            {
                using NpgsqlCommand cmd = new NpgsqlCommand(@$"insert into products(Name, Description, PhotoPath) values ('{product.Name}','{product.Description}','{product.PhotoPath}') ", connection);

                var reader = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return product;
        }
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            connection.Open();
            try
            {
                Product product = new Product();
                using NpgsqlCommand cmd = new NpgsqlCommand(@$"select * from products", connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.Name = reader.GetString(0);
                    product.Description = reader.GetString(1);
                    product.PhotoPath = reader.GetString(2);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();

            return products;
        }
    }
}
