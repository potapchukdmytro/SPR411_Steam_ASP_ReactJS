const initState = {
    isAuth: false,
    user: null
}

export const authReducer = (state = initState, action) => {
    switch (action.type) {
        case "LOGIN":
            return {...state, isAuth: true, user: action.payload};
        case "LOGOUT":
            return {...state, isAuth: false, user: null};
        default:
            return state;
    }
}