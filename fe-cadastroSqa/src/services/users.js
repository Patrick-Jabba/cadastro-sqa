export default (httpClient) => ({
  getUsers: async(url) => {
    const response = await httpClient.get(url);

    return {
      data: response.data
    }
  },
  getById: async(id) => {
    const response = await httpClient.get(`/Usuario/${id}`);

    return {
      data: response.data
    }
  },
  addUser: async(user) => {
    const response = await httpClient.post(`/Usuario`, user);

    return {
      data: response.data
    }
  },
  updateUser: async(user) => {
    const response = await httpClient.put(`/Usuario/${user.id}`, user);

    return {
      data: response.data
    }
  },
  removeUser: async (id) => {
    await httpClient.delete(`/Usuario/${id}`)
  }
})