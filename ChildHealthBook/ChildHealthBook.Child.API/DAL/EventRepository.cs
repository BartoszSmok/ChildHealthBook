using AutoMapper;
using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common.WebDtos.EventDtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.DAL
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<ExaminationModel> _examinations;
        private readonly IMongoCollection<PersonalEventModel> _personalEvents;
        private readonly IMongoCollection<MedicalEventModel> _medicalEvents;
        private readonly IMongoCollection<ShareEventModel> _sharedEvents;
        private readonly IMapper _mapper;

        public EventRepository(IChildApiSettings apiSettings, IMapper mapper)
        {
            var client = new MongoClient(apiSettings.ConnectionString);
            var database = client.GetDatabase(apiSettings.DatabaseName);
            _examinations = database.GetCollection<ExaminationModel>(apiSettings.ExaminationCollectionName);
            _personalEvents = database.GetCollection<PersonalEventModel>(apiSettings.PersonalEventCollectionName);
            _medicalEvents = database.GetCollection<MedicalEventModel>(apiSettings.MedicalEventCollectionName);
            _sharedEvents = database.GetCollection<ShareEventModel>(apiSettings.SharedEventCollectionName);

            _mapper = mapper;
        }

        public async Task AddNewExamination(string messageText)
        {
            ExaminationModel examinationModel = JsonSerializer.Deserialize<ExaminationModel>(messageText);
            await _examinations.InsertOneAsync(examinationModel);
        }

        public async Task AddNewMedicalEvent(string messageText)
        {
            MedicalEventModel medicalEventModel = JsonSerializer.Deserialize<MedicalEventModel>(messageText);
            await _medicalEvents.InsertOneAsync(medicalEventModel);
        }

        public async Task AddNewPersonalEvent(string messageText)
        {
            PersonalEventModel personalEventModel = JsonSerializer.Deserialize<PersonalEventModel>(messageText);
            await _personalEvents.InsertOneAsync(personalEventModel);
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

        public async Task<PersonalEventModel> GetChildPersonalEventById(Guid eventId)
        {
            return await _personalEvents.Find<PersonalEventModel>(personalevent => personalevent.Id == eventId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonalEventReadDto>> GetChildPersonalEvents(Guid childId)
        {
            IEnumerable<PersonalEventModel> result = await _personalEvents.Find<PersonalEventModel>(personalevent => personalevent.ChildId == childId).ToListAsync();
            return _mapper.Map<IEnumerable<PersonalEventReadDto>>(result);
        }

        public async Task<IEnumerable<ShareEventModel>> GetSharedEventByParentId(Guid parentId)
        {
            return await _sharedEvents.Find<ShareEventModel>(sharedevent => sharedevent.ParentId == parentId).ToListAsync();
        }

        public async Task ShareEvent(string messageText)
        {
            ShareEventModel shareEventModel = JsonSerializer.Deserialize<ShareEventModel>(messageText);
            await _sharedEvents.InsertOneAsync(shareEventModel);
        }
    }
}
