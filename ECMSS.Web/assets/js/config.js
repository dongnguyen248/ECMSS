const api = axios.create({
    baseURL: "https://localhost:44372/api/",
    timeout: 5000,
});

const router = new Router({
    mode: 'history'
});

const EMPLOYEE_ID = 1;