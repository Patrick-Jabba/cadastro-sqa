import { createStore, useStore as vuexUseStore } from 'vuex';

import { user } from "./modules/user";

export const store = createStore({
  state: {
    user: {
     users: [],
     updateUser: {},
     totalPaginas: 0
    }
  },
  modules: {
    user
  }
});

export default function useStore() {
  return vuexUseStore();
}
