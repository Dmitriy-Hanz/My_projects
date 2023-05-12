package populationcensus.controllers.rest;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import populationcensus.repository.entity.security.User;
import populationcensus.service.interfaces.security.UserService;

@RestController
@RequiredArgsConstructor
@RequestMapping("/userRest")
public class UserRestController {

    private final UserService userService;

    @PostMapping
    public ResponseEntity<?> create(@RequestBody User user) {
        userService.saveUser(user);
        return new ResponseEntity<>(HttpStatus.CREATED);
    }

    @PutMapping
    public ResponseEntity<?> update(@RequestBody User user) {
        userService.saveUser(user);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping
    public ResponseEntity<?> delete(@PathVariable(name = "id") long id) {
        userService.deleteUserById(id);
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
