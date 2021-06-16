using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Child.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChildHealthBook.Common.WebDtos.EventDtos;

namespace ChildHealthBook.Child.API.DAL
{
    public class ChildRepository : IChildRepository
    {
        private readonly IMongoCollection<ChildModel> _children;
        private readonly IMongoCollection<ExaminationModel> _examinations;
        private readonly IMongoCollection<PersonalEventModel> _personalEvents;
        private readonly IMongoCollection<MedicalEventModel> _medicalEvents;
        private readonly IMapper _mapper;

        public ChildRepository(IApiSettings apiSettings, IMapper mapper)
        {
            var client = new MongoClient(apiSettings.ConnectionString);
            var database = client.GetDatabase(apiSettings.DatabaseName);
            _children = database.GetCollection<ChildModel>(apiSettings.ChildCollectionName);
            _examinations = database.GetCollection<ExaminationModel>(apiSettings.ExaminationCollectionName);
            _personalEvents = database.GetCollection<PersonalEventModel>(apiSettings.PersonalEventCollectionName);
            _medicalEvents = database.GetCollection<MedicalEventModel>(apiSettings.MedicalEventCollectionName);

            _mapper = mapper;
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            var newChild = _mapper.Map<ChildModel>(childCreateDto);
            await _children.InsertOneAsync(newChild);
        }

        public async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            var result = await _children.Find<ChildModel>(childModel => true).ToListAsync();
            return _mapper.Map<IEnumerable<ChildReadDto>>(result);
        }

        public async Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            var result = await _children.Find<ChildModel>(childmodel => childmodel.Id == childId).FirstOrDefaultAsync();
            return _mapper.Map<ChildWithEventsReadDto>(result);
        }

        public async Task<IEnumerable<MedicalExaminationReadDto>> GetChildExaminations(Guid childId)
        {
            IEnumerable<ExaminationModel> result = await _examinations.Find<ExaminationModel>(examination => examination.ChildId == childId).ToListAsync();
            return _mapper.Map<IEnumerable<MedicalExaminationReadDto>>(result);
        }

        public async Task<IEnumerable<MedicalEventReadDto>> GetChildMedicalEvents(Guid childId)
        {
            IEnumerable<MedicalEventModel> result = await _medicalEvents.Find<MedicalEventModel>(medicalevent => medicalevent.ChildId == childId).ToListAsync();
            return _mapper.Map<IEnumerable<MedicalEventReadDto>>(result);
        }

        public async Task<IEnumerable<PersonalEventReadDto>> GetChildPersonalEvents(Guid childId)
        {
            IEnumerable<PersonalEventModel> result = await _personalEvents.Find<PersonalEventModel>(personalevent => personalevent.ChildId == childId).ToListAsync();
            return _mapper.Map<IEnumerable<PersonalEventReadDto>>(result);
        }

        //public List<Material> GetAll() => _materials.Find(material => true).ToList();
        //public Material GetById(Guid id) => _materials.Find<Material>(material => material.Id == id).FirstOrDefault();
        //public Material Create(Material material) { _materials.InsertOne(material); return material; }
        //public void Update(Guid id, Material materialIn) => _materials.ReplaceOne(material => material.Id == id, materialIn);
        //public void Remove(Material materialIn) => _materials.DeleteOne(material => material.Id == materialIn.Id);
        //public void Remove(Guid id) => _materials.DeleteOne(material => material.Id == id);
    }
}
