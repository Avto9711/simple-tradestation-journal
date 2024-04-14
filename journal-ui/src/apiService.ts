import config from "./config";
import useUserStore from '@/stores/user'
class ApiService
{

    public async getAccountGeneralBalance(account:string)
    {
        const response = await this.getRequest(`${config.apiUrl}api/Journal/GeneralBalance/${account}`);
        return await response.json()
    }

    public async getAccountHistoricalJournalBalance(account:string)
    {
        const response = await this.getRequest(`${config.apiUrl}api/Journal/Historical/${account}`);
        return await response.json()
    }

    public async getUserAccessToken(code:string, redirectUrl:string): Promise<Record<string, any>>{
        const response = await this.postRequest(`${config.apiUrl}api/Authentication/Token`, {code, redirectUrl});
        return await response.json()
    }

    public async getUserAccounts(): Promise<Record<string, any>[]>{
        const response = await this.getRequest(`${config.apiUrl}api/Accounts`);
        return await response.json()
    }

    private async getRequest(url:string):Promise<any>
    {
        const options:RequestInit  = {method:'GET', 
        headers: {
        "Content-Type": "application/json",
        ...this.getAuthorizationHeader()
        },
        credentials: 'include',
    };

     return await fetch(url,options);
    }

    private async postRequest(url:string, body:Record<string, any>):Promise<Response>
    {
    const options:RequestInit = {
        method:'POST', 
        body: JSON.stringify(body), 
        headers: {
        "Content-Type": "application/json",
        ...this.getAuthorizationHeader()
        }
    }
     return await fetch(url,options)
    }

    private getAuthorizationHeader(): Record<string, any>{
     const userStore = useUserStore()
     const token = userStore.bearerToken;

     return token === null ? {} : {'Authorization': `Bearer ${token}`};
    }
}

const apiService = new ApiService()

export default apiService;