export default class BaseService {
    _baseUrl = 'api/';

    getResource = async url => {
        const call = _url => fetch(this._baseUrl + _url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
            }),
        });

        let res = await call(url);
        // if (res.status === 401 && await this.refreshHandler()) {
        //     // one more try:
        //     res = await call(url);
        // }
        return res;
    }
    deleteResource = async url => {
        const call = _url => fetch(this._baseUrl + _url, {
            method: "delete",
            headers: new Headers({
                'Content-Type': 'application/json',
            }),
        });

        let res = await call(url);
        return res;
    }

    updateResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "put",
                headers: new Headers({
                    'Content-Type': 'application/json',
                }),
                body: JSON.stringify(data)
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }
    
    setResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json',
                }),
                body: JSON.stringify(data)
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }

    setResourceWithData = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                   
                }),
                body: data
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }

    refreshHandler = async () => {
        let response = await fetch('api/token/refresh-token', {
            method: "POST"
        });

        if (!response.ok) {
            return false;
        }

        let rest = await response.json();

        return true;
    }
}
