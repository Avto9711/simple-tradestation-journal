import config from "./config";

class ApiService
{
    public async getUserAccessToken(code:string, redirectUrl:string): Promise<Record<string, any>>{
        return await this.postRequest(`${config.apiUrl}api/Authentication/Token`, {code, redirectUrl})
    }

    private async getRequest(url:string):Promise<any>
    {
     return await fetch(url,{method:'GET'})
    }

    private async postRequest(url:string, body:Record<string, any>):Promise<any>
    {
     return await fetch(url,{method:'POST', body: JSON.stringify(body)})
    }
}

const apiService = new ApiService()

export default apiService;