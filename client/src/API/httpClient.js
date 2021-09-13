import axios from 'axios';
import appConfig from '../Config/config.json';

axios.defaults.baseURL = appConfig.apiConnectionUrl;

axios.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwt');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const responseBody = (response) => response.data;

const requests = {
  get: (url) => axios.get(url).then(responseBody),
  post: (url, body) => axios.post(url, body).then(responseBody),
  put: (url, body) => axios.put(url, body).then(responseBody),
  delete: (url) => axios.delete(url).then(responseBody),
};

const Tags = {
  myTags: () => requests.get('/tags/my'),
  addToTodoSet: (tags) => requests.post('/tags/add/to/todoset', tags),
};

const Todos = {
  add: (todo) => requests.post('/todos', todo),
  addMultiple: (todos) => requests.post('/todos/list', todos),
  update: (todo, id) => requests.put(`/todos/${id}`, todo),
  get: (id) => requests.get(`/todos/${id}`),
  remove: (id) => requests.delete(`/todos/${id}`),
  stats: (days) => requests.get(`/todos/stats/${days}`),
};

const TodoSets = {
  myTodoSets: () => requests.get('/todoSets/my'),
  create: (todoSet) => requests.post('/todoSets', todoSet),
  remove: (id) => requests.delete(`/todoSets/${id}`),
};

const User = {
  login: (user) => requests.post('/users/login', user),
  currentUser: () => requests.get('/users'),
  update: (user) => requests.put('/users', user),
  facebookLogin: (accessToken) =>
    requests.post('/users/login/facebook', accessToken),
};

export default {
  Tags,
  Todos,
  TodoSets,
  User,
};
