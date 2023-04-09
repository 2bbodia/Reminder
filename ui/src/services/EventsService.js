import BaseService from './BaseService';

const baseService = new BaseService();

export default class EventsService {

    getEventsByUserId = (id,date) => {
        const searchParams = new URLSearchParams();
        searchParams.set("userId", id);
        searchParams.set("date",date);
        return baseService.getResource('Events?' + searchParams.toString());
    }


    createEvent = (userId,title,description, startDate,endDate, remindAt, importance) => {
        return baseService.setResource('Events',
            {
                userId: userId,
                title: title,
                description: description,
                startDate: startDate,
                endDate: endDate,
                remindAt: remindAt,
                importance: importance
            })
    }

    deleteEvent = (id) => {
        return baseService.deleteResource('Events/' + id);
    }

    isExist = (id, startDate,endDate) => {
        const searchParams = new URLSearchParams();
        searchParams.set("userId", id);
        searchParams.set("startDate", startDate);
        searchParams.set("endDate", endDate);
        return baseService.getResource('Events/IsExist?' + searchParams.toString());
    }
    
    
}
