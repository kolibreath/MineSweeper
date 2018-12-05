import sys
import importlib
import os
import time
from app import create_app,db
from app.models import User
from flask_script import Manager,Shell,Command
from flask_migrate import Migrate,MigrateCommand

app=create_app(os.getenv('FLASK_CONFIG') or 'default')
manager=Manager(app)
migrate=Migrate(app,db)

manager.add_command('db',MigrateCommand)

def make_shell_context():
    return dict(app=app)

manager.add_command("shell",Shell(make_context=make_shell_context))

if __name__=='__main__':
    manager.run()
    app.run(debug=True)
