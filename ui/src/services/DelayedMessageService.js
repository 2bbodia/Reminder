﻿import BaseService from './BaseService';

const baseService = new BaseService();

export default class DelayedMessageService {

    getMessagesByUserId = (id, page, pageSize,orderBy) => {
        const searchParams = new URLSearchParams();
        searchParams.set("userId", id);
        searchParams.set("page",page);
        searchParams.set("pageSize",pageSize);
        searchParams.set("orderBy", orderBy);
        return baseService.getResource('DelayedMessage' + searchParams.toString());
    }
    
    
}