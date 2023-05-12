package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import populationcensus.Consts;
import populationcensus.repository.entity.security.User;
import populationcensus.service.interfaces.security.UserService;

import java.util.List;

@Controller
@RequiredArgsConstructor
public class UsersListPageController {

    private final UserService userService;

    @GetMapping(Consts.Url.$_ADMIN_MAIN_$_USERS_LIST)
    public String load(Model model){
        List<User> users = userService.findOnlyCustom();
        model.addAttribute("users", users);
        return "usersListPage";
    }

    @PostMapping(Consts.Url.$_CREATE_USER)
    public String createUser(){
        return "redirect:" + Consts.Url.$_ADMIN_MAIN_$_USERS_LIST_$_CREATE_USER;
    }
}
