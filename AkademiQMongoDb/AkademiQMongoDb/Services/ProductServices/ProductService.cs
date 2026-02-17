using AkademiQMongoDb.DTOs.ProductDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using Mapster;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AkademiQMongoDb.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            // ✔ DOĞRU: DatabaseName kullanılmalı
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            // ✔ Doğru collection adı
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            var product = productDto.Adapt<Product>();
            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _productCollection.DeleteOneAsync(product => product.Id == id);
        }

        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            var products = await _productCollection.AsQueryable().ToListAsync();
            return products.Adapt<List<ResultProductDto>>();
        }

        public async Task<UpdateProductDto> GetByIdAsync(string id)
        {
            var product = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return product.Adapt<UpdateProductDto>();
        }

        public async Task UpdateAsync(UpdateProductDto productDto)
        {
            var product = productDto.Adapt<Product>();

            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
