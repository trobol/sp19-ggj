[GitHub Desktop](https://desktop.github.com/)
## Change existing repo to github origin

    git remote set-url origin https://github.com/trobol/sp19-ggj.git
    
to check the origin url

     git remote -v
    
## Create a new branch.

    git checkout -b <branch-name>

## To push your changes to master:
 
 COMMIT ALL CHANGES
 
    git checkout master
    git merge <branch-name>
    git push
    git checkout <branch-name>
    
    
 
