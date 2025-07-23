using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InMemoryCache
{
    public class UserChatDb
    {
        private readonly ConcurrentDictionary<string, UserChatDto> _userChat = new();
        public ConcurrentDictionary<string, UserChatDto> UserChat => _userChat;

      
        public void AddChat(string chatId, UserChatDto? chat)
        {
            if (chat is null)
            {
                _userChat.TryRemove(chatId, out _);
                return;
            }

            _userChat.AddOrUpdate(chatId,
                                  chat,              // when key is new
                                  (_, _) => chat);   // when key exists (overwrite)
        }

      
        public bool RemoveChat(string chatId) =>
            _userChat.TryRemove(chatId, out _);

        public UserChatDto? GetChat(string chatId) =>
            _userChat.TryGetValue(chatId, out var dto) ? dto : null;

        public bool TryGetChat(string chatId, out UserChatDto dto) =>
            _userChat.TryGetValue(chatId, out dto);
    }
}

