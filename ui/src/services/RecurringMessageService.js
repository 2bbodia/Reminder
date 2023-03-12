import BaseService from './BaseService';

const baseService = new BaseService();

export default class RecurringMessageService {

    getMessagesByUserId = (id, page, pageSize,orderBy) => {
        const searchParams = new URLSearchParams();
        searchParams.set("userId", id);
        searchParams.set("page",page);
        searchParams.set("pageSize",pageSize);
        searchParams.set("orderBy", orderBy);
        return baseService.getResource('RecurringMessage?' + searchParams.toString());
    }
    
    createMinutelyMessage = (text, receiverId) => {
        return baseService.setResource('RecurringMessage/Minutely',
            {
                text:text,
                receiverId :receiverId
            })
    }

    createHourlyMessage = (text, receiverId, minute) => {
        return baseService.setResource('RecurringMessage/Hourly',
            {text : text, 
                receiverId :receiverId,
                minute :minute
            })
    }
    createDailyMessage = (text, receiverId, hour,minute) => {
        return baseService.setResource('RecurringMessage/Daily',
            {text : text, 
                receiverId :receiverId,
                hour:hour,
                minute :minute
            })
    }
    createWeeklyMessage = (text, receiverId, dayOfWeek,hour,minute) => {
        return baseService.setResource('RecurringMessage/Weekly',
            {text : text, 
                receiverId :receiverId,
                dayOfWeek:dayOfWeek,
                hour:hour,
                minute :minute
            })
    }
    createMonthlyMessage = (text, receiverId, day,hour,minute) => {
        return baseService.setResource('RecurringMessage/Monthly',
            {text : text, 
                receiverId :receiverId,
                day:day,
                hour:hour,
                minute :minute
            })
    }
    createYearlyMessage = (text, receiverId, month,day,hour,minute) => {
        return baseService.setResource('RecurringMessage/Yearly',
            {text : text, 
                receiverId :receiverId,
                month:month,           
                day:day,
                hour:hour,
                minute :minute
            })
    }

    deleteMessage = (id) => {
        return baseService.deleteResource('RecurringMessage/' + id);
    }
    
    
}
