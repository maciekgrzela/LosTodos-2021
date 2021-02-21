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
  addToTaskSet: (tags) => requests.post('/tags/add/to/taskset', tags),
};

const Tasks = {
  add: (task) => requests.post('/todo', task),
  addMultiple: (tasks) => requests.post('/todo/list', tasks),
  update: (task, id) => requests.put(`/todo/${id}`, task),
  get: (id) => requests.get(`/todo/${id}`),
  remove: (id) => requests.delete(`/todo/${id}`),
  stats: (days) => requests.get(`/todo/stats/${days}`),
};

const TaskSets = {
  myTaskSets: () => requests.get('/todolist/my'),
  create: (taskSet) => requests.post('/todolist', taskSet),
  remove: (id) => requests.delete(`/todolist/${id}`),
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
  Tasks,
  TaskSets,
  User,
};
