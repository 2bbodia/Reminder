import BaseService from './BaseService';

const baseService = new BaseService();

export default class DelayedMessageService {

    getMessagesByUserId = (id) => baseService.getResource('DelayedMessage/GetAllMessagesByUserId/' + id);
    
}
