﻿using System.Text;
using System.Text.Json;
using TDC.IService;
using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.Services
{
    public class ListService : IListService
    {
        private readonly HttpClient httpClient = new();

        #region publics
        public async Task<long> CreateList(string name, bool isCollaborative, string creator)
        {
            var dto = new ToDoListCreateDto(name, isCollaborative); //TODO: Add toggle button for collaborative 
            var url = ConnectionUrls.development + $"/api/List/createList/{creator}";

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            return long.Parse(responseContent);
        }

        public async Task UpdateListTitle(string newTitle, long listId)
        {
            var url = ConnectionUrls.development + $"/api/List/updateListTitle/{listId}";
            var data = new
            {
                newTitle = newTitle,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
        }

        public async Task DeleteList(long listId, string sender)
        {
            var url = ConnectionUrls.development + $"/api/List/deleteList/{listId}";
            var data = new
            {
                username = sender,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(url, content);
        }

        public async Task FinishList(long listId, string sender)
        {
            var url = ConnectionUrls.development + $"/api/List/finishList/{listId}";
            var data = new
            {
                sender = sender,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
        }

        public async Task<ToDoList> GetListById(long listId)
        {
            var url = ConnectionUrls.development + $"/api/List/getListById/{listId}";

            var response = await httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var listDto = JsonSerializer.Deserialize<ToDoListDto>(responseContent)!;

            return new ToDoList(listDto.ListId, listDto.Name, listDto.IsCollaborative);
        }

        public async Task<List<ToDoList>> GetAllListsForUser(string username)
        {
            var url = ConnectionUrls.development + $"/api/List/getListsForUser/{username}";

            var response = await httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var listDtos = JsonSerializer.Deserialize<List<ToDoListDto>>(responseContent)!;

            var listObjects = new List<ToDoList>();
            foreach(var listDto in listDtos)
            {
                var list = new ToDoList(listDto.ListId, listDto.Name, listDto.IsCollaborative);
                listObjects.Add(list);
            }
            return listObjects;
        }
        #endregion


    }
}
