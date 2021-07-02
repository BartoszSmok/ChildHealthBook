using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Data;
using ChildHealthBook.Notification.Service.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ChildHealthBook.Notification.Service.Services
{
    public class MessagesHandler
    {
        private readonly IMsgContext _context;
        private HttpClient _httpClient;

        public MessagesHandler(IMsgContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        public async Task AddMsgToDb(MsgToSend msg)
        {
            await _context.msg.InsertOneAsync(msg);
        }

        public async Task<IEnumerable<MsgToSend>> GetMsgToSend()
        {
            return await _context.msg.Find(m => true).ToListAsync();
        }

        public async Task DeleteMsg(MsgToSend msg)
        {
            await _context.msg.DeleteOneAsync(m => m.id == msg.id);
        }

        public void DeleteOldMsg()
        {
        }

        public async Task<MsgToSend> UpdateMsgToSendWithParentInfo(MsgToSend msg)
        {
            var parentInfo =  await GetParentInfoAsync(msg.msg);
            if (parentInfo != null)
            {
                msg.parent = parentInfo;
                //await _context.msg.
                return msg;
            }

            return null;
        }

        public MsgToSend CreateMsgToSend(ExaminationNotificationDto msg, ParentsDto parentInfo)
        {
            var msgToSend = new MsgToSend();
            if (parentInfo == null)
            {
                 msgToSend.msg = msg;
                 return msgToSend;
            }
            msgToSend.parent = parentInfo;
            msgToSend.msg = msg;
            return msgToSend;
        }

        public async Task<ParentsDto> GetParentInfoAsync(ExaminationNotificationDto msg)
        {
            //var parent = new ParentsDto() {Email = "smok.bartosz@gmail.com"};
            var response = await _httpClient.GetAsync($"https://identityapi-service/api/Accounts/user/getUsers/{msg.ParentId}");
            var json = await response.Content.ReadAsStringAsync();
            var parent = JsonConvert.DeserializeObject<ParentsDto>(json);
            if(parent == null)
                return null;
            return parent;
        }

    }
}
