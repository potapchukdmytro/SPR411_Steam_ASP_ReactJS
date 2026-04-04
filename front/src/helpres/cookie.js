export const setCookie = (name, value, expires = null) => {
    if (expires) {
        document.cookie = `${name}=${value}; expires=${expires}; path=/;`;
    } else {
        document.cookie = `${name}=${value}; path=/;`;
    }
}

export const getCookie = (name) => {
    let nameEQ = name + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i].trim();
        if (c.indexOf(nameEQ) == 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}

export const removeCookie = (name) => {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
}