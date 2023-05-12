package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.SessionAttributes;
import populationcensus.Consts;
import populationcensus.dto.security.UserDto;
import populationcensus.dto.mapper.UserMapper;
import populationcensus.repository.entity.security.Role;
import populationcensus.repository.entity.security.User;
import populationcensus.service.interfaces.security.RoleService;
import populationcensus.service.interfaces.security.UserService;

import javax.servlet.http.HttpServletRequest;
import javax.validation.Valid;
import java.util.Set;

@Controller
@RequiredArgsConstructor
@SessionAttributes("currentUser")
public class CreateEditUserPageController {

    private final UserService userService;
    private final RoleService roleService;
    private final UserMapper userMapper;

    @GetMapping(Consts.Url.$_ADMIN_MAIN_$_USERS_LIST_$_CREATE_USER)
    public String loadCreate(Model model) {
        model.addAttribute("user", new UserDto());
        return "createEditUserPage";
    }

    @GetMapping("/adminMain/editUser")
    public String loadEdit() {
        return "createEditUserPage";
    }

    @PostMapping("/adminMain/editUser")
    public String saveEditedUser(@ModelAttribute(name = "currentUser") @Valid UserDto obj, BindingResult bindingResult, HttpServletRequest req) {
        if (bindingResult.hasErrors()) {
            return "createEditUserPage";
        }
        User currentUser = userMapper.toUser(obj);
        userService.updateUser(currentUser);
        return "redirect:" + previousPage(req);
    }

    @PostMapping(Consts.Url.$_ADMIN_MAIN_$_USERS_LIST_$_CREATE_USER)
    public String saveCreatedUser(@ModelAttribute(name = "user") @Valid UserDto obj, BindingResult bindingResult, HttpServletRequest req) {
        if (bindingResult.hasErrors()) {
            return "createEditUserPage";
        }
        User user = userMapper.toUser(obj);
        Role role = roleService.findByName("ROLE_ADMIN");
        user.setRoles(Set.of(role));
        userService.saveUser(user);
        return "redirect:" + previousPage(req);
    }

    @PostMapping("/cancel")
    public String cancel(HttpServletRequest req) {
        return "redirect:" + previousPage(req);
    }


    private String previousPage(HttpServletRequest req) {
        return req.getHeader("referer")
                .replaceAll("/createUser", "")
                .replaceAll("/editUser", "");
    }
}
