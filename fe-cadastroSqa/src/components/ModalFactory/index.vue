<template>
  <Teleport to="body">
    <div
      v-if="state.isActive"
      class="modal"
      @click="handleModalToggle({ status: false })"
    >
      <div class="modal-body" :class="state.width" @click.stop>
        <div
          class="modal-content">
          <div class="modal-component">
            <component :is="state.component" />
          </div>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<script>
import {
  onBeforeUnmount,
  onMounted,
  reactive,
  defineAsyncComponent,
} from "vue";

import useModal from "../../hooks/useModal";

const ModalAddUser = defineAsyncComponent(() => import("../ModalAddUser/index.vue"));
const ModalUpdateUser = defineAsyncComponent(() => import("../ModalUpdateUser/index.vue"));

const DEFAULT_WIDTH = "75%";

export default {
  components: {
    ModalAddUser,
    ModalUpdateUser
  },

  setup() {
    const modal = useModal();
    const state = reactive({
      isActive: false,
      component: {},
      props: {},
      width: DEFAULT_WIDTH,
    });

    onMounted(() => {
      modal.listen(handleModalToggle);
    });

    onBeforeUnmount(() => {
      modal.off(handleModalToggle);
    });

    function handleModalToggle(payload) {
      if (payload.status) {
        state.component = payload.component;
        state.props = payload.props;
        state.width = payload.width ?? DEFAULT_WIDTH;
      } else {
        state.component = {};
        state.props = {};
        state.width = DEFAULT_WIDTH;
      }

      state.isActive = payload.status;
    }
    return {
      state,
      handleModalToggle,
    };
  },
};
</script>

<style scoped>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 50;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
  background-color: #111;
  opacity: 0.98;
}

.modal-body {
  position: fixed;
}

.modal-content{
  display: flex;
  flex-direction: column;
  overflow: hidden;
  background-color: #f8f8f2;
  border-radius: 0.5rem;
  margin-top: 1rem;
  width: 46rem;
  padding: 1rem 0;
}

.modal-component{
  display: flex;
  flex-direction: column;
  background-color: #E0E3FF;
  gap: 2vh;
}
</style>
