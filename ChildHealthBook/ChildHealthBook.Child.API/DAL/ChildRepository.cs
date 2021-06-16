using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Child.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ChildHealthBook.Child.API.DAL
{
    public class ChildRepository : IChildRepository
    {
        private readonly IMongoCollection<ChildModel> _children;
        private readonly IMapper _mapper;

        public ChildRepository(IApiSettings apiSettings, IMapper mapper)
        {
            var client = new MongoClient(apiSettings.ConnectionString);
            var database = client.GetDatabase(apiSettings.DatabaseName);
            _children = database.GetCollection<ChildModel>(apiSettings.ChildCollectionName);

            _mapper = mapper;
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            var newChild = _mapper.Map<ChildModel>(childCreateDto);
            await _children.InsertOneAsync(newChild);
        }

        public async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            var result = await _children.Find(ChildModel => true).ToListAsync();
            return _mapper.Map<IEnumerable<ChildReadDto>>(result);
        }

        //public List<Material> GetAll() => _materials.Find(material => true).ToList();
        //public Material GetById(Guid id) => _materials.Find<Material>(material => material.Id == id).FirstOrDefault();
        //public Material Create(Material material) { _materials.InsertOne(material); return material; }
        //public void Update(Guid id, Material materialIn) => _materials.ReplaceOne(material => material.Id == id, materialIn);
        //public void Remove(Material materialIn) => _materials.DeleteOne(material => material.Id == materialIn.Id);
        //public void Remove(Guid id) => _materials.DeleteOne(material => material.Id == id);
    }
}
