import { createContext, useContext, useEffect, useState, type Dispatch, type SetStateAction } from "react";

type UserContextProps = {
    user: User | null,
    setUser: Dispatch<SetStateAction<User | null>>
}
const UserContext = createContext({} as UserContextProps);

export type User = {
    id: string,
    name: string,
    email: string,
}


export const UserContextProvider = ({children}: {children: React.ReactNode}) => {
    const [user, setUser] = useState<User | null>(null)

    const getMe = async () => {
        const resp = await fetch("/api/user/getme", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include"
        })

        console.log(resp);
        
    }

    useEffect(() => {
        getMe()
    }, [])

    return (
        <UserContext.Provider value={{user, setUser}}>
            {children}
        </UserContext.Provider>
    )
}

export const useUser = () => {
    return useContext(UserContext)
}