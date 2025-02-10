import axios from "axios"

export async function LogInUser(){
    try{
        const response = await axios.post (`/api/Login`)
        return response.data
    }
    catch(error){
        throw new Error(`Failed to create order: ${error}`)
    }
}
