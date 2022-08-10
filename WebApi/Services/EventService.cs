using WebApi.Enums;
using WebApi.Models.Event;

namespace WebApi.Services
{
    public interface IEventService
    {
        Task<List<Event>> getAll();
        Task<Event> getById(int id);
        Task<Event> addAsync(EventDto eventDto, Account currentUser);
        Task<Event> updateAsync(int id, EventDto eventDto, Account currentUser);
        Task<Event> deleteAsync(int id, Account currentUser);
    }
    public class EventService : IEventService
    {
        private readonly DataContext _context;

        public EventService(DataContext context)
        {
            _context = context;
        }

        public async Task<Event> addAsync(EventDto eventDto, Account currentUser)
        {
            Event newEvent = new Event();

            newEvent.Name = eventDto.Name;
            newEvent.Description = eventDto.Description;
            newEvent.Status = (int)StatusEnum.DRAFT;
            newEvent.StartedDate = eventDto.StartDate;
            newEvent.EndDate = eventDto.EndDate;
            newEvent.CreatedDate = DateTime.Now;
            newEvent.UpdatedDate = DateTime.Now;
            newEvent.CreatedAccount = currentUser;
            newEvent.UpdatedAccount = currentUser;
            
            this._context.Add(newEvent);
            await this._context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event> deleteAsync(int id, Account currentUser)
        {
            Event existEvent = await this.getById(id);
            if (existEvent != null)
            {
                existEvent.Status = (int)StatusEnum.DELETED;
                existEvent.UpdatedDate = DateTime.Now;
                existEvent.UpdatedAccount = currentUser; 
                this._context.Entry(existEvent).State = EntityState.Modified;
                await this._context.SaveChangesAsync();
            }
            return existEvent;
        }

        public async Task<List<Event>> getAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> getById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> updateAsync(int id, EventDto eventDto, Account currentUser)
        {
            Event existEvent = await this.getById(id);

            if(existEvent != null)
            {
                existEvent.Name = eventDto.Name;
                existEvent.Description = eventDto.Description;
                existEvent.UpdatedDate = DateTime.Now;
                existEvent.UpdatedAccount = currentUser;
                existEvent.StartedDate = eventDto.StartDate;
                existEvent.EndDate = eventDto.EndDate;

                this._context.Entry(existEvent).State = EntityState.Modified;
                this._context.Update(existEvent);
                await this._context.SaveChangesAsync();
            }
            
            return existEvent;
        }
    }
}
