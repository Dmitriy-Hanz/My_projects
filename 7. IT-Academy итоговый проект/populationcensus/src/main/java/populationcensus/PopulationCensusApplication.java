package populationcensus;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication//(scanBasePackages = {"com.other.packages","com.other"})
public class PopulationCensusApplication {

    public static void main(String[] args) {
        SpringApplication.run(PopulationCensusApplication.class, args);
    }

}
