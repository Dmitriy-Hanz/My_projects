package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;
import populationcensus.dto.mapper.UserMapper;
import populationcensus.dto.security.UserDto;
import populationcensus.repository.entity.security.User;
import populationcensus.service.impl.security.UserDetailsImpl;
import populationcensus.service.interfaces.security.UserService;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

@Controller
@RequiredArgsConstructor
@SessionAttributes("currentUser")
public class AdminMainPageController {

    private final UserService userService;
    private final UserMapper userMapper;

    @GetMapping("/adminMain")
    public String load(Model model){
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        User authenticatedUser = ((UserDetailsImpl)authentication.getPrincipal()).getUser();
        User currentUser = userService.findById(authenticatedUser.getId());
        if (currentUser.getUsername().equals("admin")){
            model.addAttribute("currentUser", userMapper.toSystemUserDto(currentUser));
            return "adminMainPage";
        }
        model.addAttribute("currentUser", userMapper.toUserDto(currentUser));
        return "adminMainPage";
    }

    @PostMapping(value = "/navAdminMain")
    public String navAdminMainPage(){
        return "redirect:adminMain";
    }
    @PostMapping(value = "/navUsersList")
    public String navUsersListPage(){
        return "redirect:adminMain/usersList";
    }
    @PostMapping(value = "/navPersonsList")
    public String navPersonsListPage(){
        return "redirect:adminMain/personsList";
    }
    @PostMapping(value = "/navCensusResults")
    public String navCensusResultsPage(){
        return "redirect:adminMain/censusResults";
    }



    @PostMapping(value = "/deleteCurrentUser")
    public String deleteCurrentUser(Model model){
        User user = userMapper.toUser((UserDto) model.getAttribute("currentUser"));
        userService.deleteUser(user);
        return "redirect:/customLogout";
    }

    @PostMapping(value = "/editCurrentUser")
    public String editCurrentUser(){
        return "redirect:/adminMain/editUser";
    }


//    @PostMapping("/logout")
//    public String logoutPost(HttpServletRequest request, HttpServletResponse response){
//        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
//        if (auth != null){
//            new SecurityContextLogoutHandler().logout(request, response, auth);
//        }
//        return "redirect:main";
//    }
    @GetMapping("/customLogout")
    public String logoutGet(HttpServletRequest request, HttpServletResponse response){
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        if (auth != null){
            new SecurityContextLogoutHandler().logout(request, response, auth);
        }
        return "redirect:main";
    }
}
