/**
 * @vitest-environment happy-dom
 */

import { mount } from "@vue/test-utils";
import Home from "./Home.vue";

test("Home", () => {
  it('deve montar Home', async () => {
    expect(Home).toBeTruthy();
    
    const wrapper = mount(Home)

    expect(wrapper.text()).toContain("Novo cadastro");
  });
  it('click do botão novo cadastro', async () => {
    expect(Home).toBeTruthy();
    
    const wrapper = mount(Home)

    expect(wrapper.get("button")).trigger("click");
  });

})