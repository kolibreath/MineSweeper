"""empty message

Revision ID: 54a4972dc3c6
Revises: 1c1ff25672ad
Create Date: 2018-12-10 16:43:52.856347

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '54a4972dc3c6'
down_revision = '1c1ff25672ad'
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.add_column('users', sa.Column('challeng_map', sa.Text(), nullable=True))
    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.drop_column('users', 'challeng_map')
    # ### end Alembic commands ###
