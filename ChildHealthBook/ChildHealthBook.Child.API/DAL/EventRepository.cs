using AutoMapper;
using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common;
using ChildHealthBook.Common.WebDtos.EventDtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.DAL
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<ExaminationModel> _examinations;
        private readonly IMongoCollection<PersonalEventModel> _personalEvents;
        private readonly IMongoCollection<MedicalEventModel> _medicalEvents;
        private readonly IMapper _mapper;

        public EventRepository(IApiSettings apiSettings, IMapper mapper)
        {
            var client = new MongoClient(apiSettings.ConnectionString);
            var database = client.GetDatabase(apiSettings.DatabaseName);
            _examinations = database.GetCollection<ExaminationModel>(apiSettings.ExaminationCollectionName);
            _personalEvents = database.GetCollection<PersonalEventModel>(apiSettings.PersonalEventCollectionName);
            _medicalEvents = database.GetCollection<MedicalEventModel>(apiSettings.MedicalEventCollectionName);

            _mapper = mapper;
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
    }
}
