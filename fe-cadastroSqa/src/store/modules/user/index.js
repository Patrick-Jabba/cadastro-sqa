import services from "../../../services";
import Swal from "sweetalert2";
import { store } from "../../../store";

import { CREATE_USER, DELETE_USER, GET_USERS, UPDATE_DATA_USER, UPDATE_USER } from "../../type-actions";
import { ADD_USER, CHANGE_DATA_USER, CHANGE_USER, REMOVE_USER, SET_USER, SET_USERS_TOTAL_PAGES } from "../../type-mutations";

export const user = {
  mutations: {
    [ADD_USER](state, user){
      state.users.push(user)
    },
    [SET_USER](state, users){
      state.users = users
    },
    [REMOVE_USER](state, id){
      state.users = state.users.filter(u => u.id != id);
    },
    [CHANGE_DATA_USER](state, user){
      state.updateUser = user;
    },
    [CHANGE_USER](state, user){
      const index = state.users.findIndex(u => u.id === user.id);
      state.users[index] = user;
    },
    [SET_USERS_TOTAL_PAGES](state, totalPaginas){
      state.totalPaginas = totalPaginas;
    }
  },
  actions: {
    async [GET_USERS]({commit}, pagina){
      try {
        let url = "/Usuario";
        if(pagina) {
          url += `/pagina/${pagina}/itensPorPagina/15`;
        }

        const { data } = await services.users.getUsers(url);

        commit(SET_USER, data.conteudos);
        commit(SET_USERS_TOTAL_PAGES, data.totalPaginas);
        return data.conteudos;
      } catch (error) {
        console.log(error)
      }
    },
    async [UPDATE_USER]({commit}, user){
      try {
        const { data } = await services.users.updateUser(user);

        commit(CHANGE_USER, data);

        return data;
      } catch (error) {
        const response = error.response.data;

        const errorMessage = response.errors?.CPF?.[0] || response.errors?.Name?.[0] || response?.title || response;

        await Swal.fire({
          position: "center",
          icon: "warning",
          iconColor: "white",
          color: "white",
          background: "#5061FC",
          title: `Falha!`,
          text: errorMessage,
          showConfirmButton: false,
          timer: 2500,
        });
        console.log(error);
      }
    },

    async [CREATE_USER]({ commit },user){
      try {
        const { data } = await services.users.addUser(user);

        commit(ADD_USER, data);
        
        await store.dispatch(GET_USERS);
        return data;
      } catch (error) {
        const response = error.response.data;

        const errorMessage = response.errors?.CPF?.[0] || response.errors?.Name?.[0] || response?.title || response;

        await Swal.fire({
          position: "center",
          icon: "warning",
          iconColor: "white",
          color: "white",
          background: "#5061FC",
          title: `Falha!`,
          text: errorMessage,
          showConfirmButton: false,
          timer: 2500,
        });
        console.log(error);
      }
    },
    async [DELETE_USER]({commit}, id){
      try {
        await services.users.removeUser(id);

        commit(REMOVE_USER, id);
      } catch (error) {
        console.log(error);
      }
    },
    [UPDATE_DATA_USER]({commit}, user){
      commit(CHANGE_DATA_USER, user);
    }

  }
}


